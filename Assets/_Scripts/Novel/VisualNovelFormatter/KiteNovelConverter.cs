using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Assets._Scripts.Novel;
using Assets._Scripts.Player.KiteNovels.VisualNovelFormatter;
using Assets._Scripts.Novel.VisualNovelFormatter;
using UnityEngine;

namespace Assets._Scripts.Player.KiteNovels.VisualNovelFormatter
{
    #region NovelKeywordModel and Parser

    /// <summary>
    /// Model for a keyword. All fields are optional.
    /// </summary>
    public class NovelKeywordModel
    {
        public int? CharacterIndex { get; set; }
        public string Action { get; set; }
        public string FaceExpression { get; set; }
        public string Bias { get; set; }
        public string Sound { get; set; }
        public bool? End { get; set; }
    }

    /// <summary>
    /// Parser that converts a keyword string (e.g. ">>Character1|Looks|Angry<<") into a NovelKeywordModel.
    /// Expected formats:
    ///   >>End<<                   → sets End = true
    ///   >>Info<<                  → sets CharacterIndex = 0
    ///   >>Player<<                → sets CharacterIndex = 1
    ///   >>Character1|Looks|Angry<< → sets CharacterIndex = 1+1 = 2, Action = "Looks", FaceExpression = "Angry"
    ///   >>Sound|TestSound<<        → sets Sound = "TestSound"
    ///   >>Bias|ConfirmationBias<<  → sets Bias = "ConfirmationBias"
    /// </summary>
    public static class NovelKeywordParser
    {

        // Konstante zum Dateinamen des Mapping-Files.
        private static readonly string MappingFileFullPath = Path.Combine(Application.dataPath, "Assets/_novels_twee/Mappings/BiasMapping.txt");

        // Dictionary, das das Bias-Mapping enthält. Dieses wird beim ersten Zugriff über LoadBiasMapping() geladen.
        private static Dictionary<string, string> biasMapping = LoadBiasMapping();

        private static Dictionary<string, string> LoadBiasMapping()
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                // Verwende den bereits vollständig definierten MappingFileFullPath.
                if (!File.Exists(MappingFileFullPath))
                {
                    Debug.LogWarning("Bias mapping file not found at: " + MappingFileFullPath);
                    return mapping; // Leeres Mapping zurückgeben.
                }

                // Lese alle Zeilen der Datei.
                string[] lines = File.ReadAllLines(MappingFileFullPath);
                foreach (string line in lines)
                {
                    // Überspringe leere oder nur aus Leerzeichen bestehende Zeilen.
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Suche nach dem ersten Doppelpunkt als Trenner.
                    int colonIndex = line.IndexOf(':');
                    if (colonIndex > 0 && colonIndex < line.Length - 1)
                    {
                        // Extrahiere den englischen Bias (Key) und den deutschen Bias (Value).
                        string key = line.Substring(0, colonIndex).Trim();
                        string value = line.Substring(colonIndex + 1).Trim();
                        if (!string.IsNullOrEmpty(key) && !mapping.ContainsKey(key))
                        {
                            mapping.Add(key, value);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Invalid mapping line: " + line);
                    }
                }
                Debug.Log("Loaded " + mapping.Count + " bias mappings.");
            }
            catch (Exception ex)
            {
                Debug.LogError("Error loading bias mapping: " + ex.Message);
            }
            return mapping;
        }


        /// <summary>
        /// Liest den Inhalt einer Datei (als String), verarbeitet jede Zeile und gibt
        /// eine Liste aller gültigen NovelKeywordModel-Objekte zurück. Gleichzeitig wird
        /// in der Konsole ausgegeben, wie viele gültige Keywords gefunden wurden.
        /// </summary>
        /// <param name="fileContent">Der gesamte Text aus der Keyword-Datei.</param>
        /// <returns>Liste der NovelKeywordModel für alle Zeilen, die gültige Keywords darstellen.</returns>
        public static List<NovelKeywordModel> ParseKeywordsFromFile(string fileContent)
        {
            // Teile den Inhalt in einzelne Zeilen auf (Zeilenumbrüche können \n und/oder \r sein)
            string[] lines = fileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<NovelKeywordModel> models = new List<NovelKeywordModel>();

            // Gehe alle Zeilen durch
            foreach (string line in lines)
            {
                // Rufe den Einzelzeilen-Parser auf
                NovelKeywordModel model = ParseKeyword(line);
                // Wenn ein gültiges Model zurückgegeben wurde (nicht null), füge es der Liste hinzu.
                if (model != null)
                {
                    models.Add(model);
                }
            }

            // Gib die Gesamtanzahl der validen Schlüsselwörter aus.
            Debug.Log("Total valid keywords found: " + models.Count);
            return models;
        }

        /// <summary>
        /// Parst einen einzelnen Keyword-String und gibt ein NovelKeywordModel zurück.
        /// Falls der String nicht den erwarteten Mustern entspricht, wird null zurückgegeben.
        /// Erwartete Formate:
        ///   >>End<< oder >>Ende<<                           → setzt End = true
        ///   >>Info<<                                       → setzt CharacterIndex = 0
        ///   >>Player<<                                     → setzt CharacterIndex = 1
        ///   >>Character1|Speaks|Angry<<                     → setzt CharacterIndex = 1+1=2, Action="Speaks", FaceExpression="Angry"
        ///   >>Sound|TestSound<<                             → setzt Sound = "TestSound"
        ///   >>Bias|ConfirmationBias<<                       → setzt Bias = "ConfirmationBias"
        /// </summary>
        /// <param name="keyword">Die Zeile, die verarbeitet werden soll.</param>
        /// <returns>Ein NovelKeywordModel oder null, wenn kein gültiges Keyword erkannt wurde.</returns>
        public static NovelKeywordModel ParseKeyword(string keyword)
        {
            //Debug.Log($"ParseKeyword: {keyword}");
            // Falls der Eingabestring leer oder nur Whitespace ist, wird null zurückgegeben.
            if (string.IsNullOrWhiteSpace(keyword))
            {
                Debug.Log("Empty or whitespace line encountered.");
                return null;
            }

            // Entferne führende und abschließende Leerzeichen.
            keyword = keyword.Trim();

            // Es werden nur Zeilen verarbeitet, die mit ">>" beginnen und mit "<<" enden.
            if (!(keyword.StartsWith(">>") && keyword.EndsWith("<<")))
            {
                // Keine gültigen Markierungen gefunden → Zeile überspringen.
                return null;
            }

            // Entferne die Marker ">>" und "<<".
            keyword = keyword.Substring(2, keyword.Length - 4);

            // Hinweis: Wir erzeugen vorerst kein Modell, sondern prüfen nur die Zeile
            // und erstellen später (nur) ein Modell, wenn ein gültiges Muster erkannt wurde.

            // Prüfe, ob das Keyword ein End-Kommando signalisiert.

            NovelKeywordModel model = new NovelKeywordModel();

            if (string.Equals(keyword, "End", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(keyword, "Ende", StringComparison.OrdinalIgnoreCase))
            {
                model.End = true;
                return model;
            }

            // Prüfe auf die exakten Schlüsselwörter "Info" und "Player".
            if (string.Equals(keyword, "Info", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = 0;
                model.Action = "Looks";
                model.FaceExpression = "NeutralRelaxed";
                Debug.Log("Parsed keyword (Info): " + keyword);
                return model;
            }
            if (string.Equals(keyword, "Player", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = 1;
                model.Action = "Looks";
                model.FaceExpression = "NeutralRelaxed";
                Debug.Log("Parsed keyword (Player): " + keyword);
                return model;
            }

            // Teile den String anhand des Trennzeichens '|'.
            string[] parts = keyword.Split('|');
            if (parts.Length > 0)
            {
                // Wenn es sich um ein Character‑Keyword handelt.
                if (parts[0].StartsWith("Character", StringComparison.OrdinalIgnoreCase))
                {
                    // Beispiel: "Character1" → extrahiere die Zahl.
                    string numberPart = parts[0].Substring("Character".Length);
                    if (int.TryParse(numberPart, out int num))
                    {
                        // Falls 0 für Info und 1 für Player belegt sind, wird hier num + 1 gerechnet.
                        model.CharacterIndex = num + 1;
                    }
                    if (parts.Length > 1)
                    {
                        model.Action = parts[1];
                    }
                    if (parts.Length > 2)
                    {
                        model.FaceExpression = parts[2];
                    }
                    Debug.Log("Parsed keyword (Character): " + model.CharacterIndex + " " + model.Action + " " + model.FaceExpression);
                    return model;
                }
                // Wenn es sich um ein Sound‑Keyword handelt.
                else if (parts[0].StartsWith("Sound", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length > 1)
                    {
                        model.Sound = parts[1];
                    }
                    Debug.Log("Parsed keyword (Sound): " + keyword);
                    return model;
                }
                // Wenn es sich um ein Bias‑Keyword handelt.
                else if (parts[0].StartsWith("Bias", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length > 1)
                    {
                        // Wende das externe Mapping an.
                        model.Bias = MapBias(parts[1]);
                    }
                    Debug.Log("Parsed keyword (Bias): " + keyword);
                    return model;
                }
            }

            // Falls keiner der erwarteten Fälle eintritt, wird null zurückgegeben.
            Debug.Log("Line did not match any keyword pattern: " + keyword);
            return null;
        }

        /// <summary>
        /// Internal mapping method – looks up the English bias in the dictionary and returns the German translation.
        /// </summary>
        private static string MapBias(string englishBias)
        {
            if (biasMapping.TryGetValue(englishBias, out string germanBias))
            {
                return germanBias;
            }
            else
            {
                Debug.LogWarning("Bias mapping not found for: " + englishBias);
                return englishBias; // Fallback to original.
            }
        }
    }

    #endregion

        /// <summary>
        /// Converter class that creates VisualNovel objects from processed novel folders
        /// and converts the Twee text document into a structured event list.
        /// Instead of a huge switch-case, it now uses the NovelKeywordParser to generate a NovelKeywordModel from the passage text.
        /// All values (role, expression etc.) are handled as strings.
        /// </summary>
    public abstract class KiteNovelConverter
    {
        private static int _counterForNamingPurpose = 1;
        private const string EventDefinitionSeparator = ">>--<<";

        /// <summary>
        /// Converts processed novel folders into a list of VisualNovel objects.
        /// </summary>
        public static List<VisualNovel> ConvertFilesToNovels(List<KiteNovelFolder> folders)
        {
            List<VisualNovel> novels = new List<VisualNovel>();

            foreach (KiteNovelFolder folder in folders)
            {
                VisualNovel novel = new VisualNovel
                {
                    id = folder.NovelMetaData.IdNumberOfNovel,
                    title = folder.NovelMetaData.TitleOfNovel,
                    description = folder.NovelMetaData.DescriptionOfNovel,
                    context = folder.NovelMetaData.ContextForPrompt,
                    novelEvents = folder.NovelEventList
                };

                novels.Add(novel);
            }

            return novels;
        }

        /// <summary>
        /// Converts the content of a Twee text document into a structured event list.
        /// For each passage, the message (keyword) is extracted and converted into a NovelKeywordModel.
        /// Based on the fields set in the model, the corresponding event is created.
        /// </summary>
        public static List<VisualNovelEvent> ConvertTextDocumentIntoEventList(string tweeFile, KiteNovelMetaData metaData)
        {
            List<VisualNovelEvent> eventList = new List<VisualNovelEvent>();
            string startLabel = TweeProcessor.GetStartLabelFromTweeFile(tweeFile);
            InitializeKiteNovelEventList(metaData, eventList, startLabel);
            List<TweePassage> passages = TweeProcessor.ProcessTweeFile(tweeFile);

            foreach (TweePassage passage in passages)
            {
                // Extract the message text (i.e. the keyword) from the passage.
                string message = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage);
                Debug.Log($"passage.Label: {passage.Label}");
                Debug.Log($"message: {message}");
                
                string keyword = TweeProcessor.ExtractKeywordOutOfTweePassage(passage.Passage);

                Debug.Log("keywordString: " + keyword);
                
                // Generate a NovelKeywordModel from the message text.
                NovelKeywordModel keywordModel = NovelKeywordParser.ParseKeyword(keyword);

                // Create the corresponding VisualNovelEvent based on the model.
                VisualNovelEvent createdEvent = CreateVisualNovelEventFromKeyword(passage, message, keywordModel, metaData, eventList);

                // Check if the event creates a loop, and adjust if necessary.
                HandleLoop(createdEvent, startLabel, eventList);

                // If dialogue options are present, process them.
                HandleDialogueOptionEvent(passage, eventList, createdEvent);
            }

            return eventList;
        }

        /// <summary>
        /// Creates a VisualNovelEvent based on the NovelKeywordModel.
        /// Depending on the fields set in the model (End, Bias, Sound, or Character event),
        /// the corresponding event is created.
        /// </summary>
        private static VisualNovelEvent CreateVisualNovelEventFromKeyword(TweePassage passage, string originalMessage, NovelKeywordModel model, KiteNovelMetaData metaData, List<VisualNovelEvent> eventList)
        {
            if (model == null) return null;

            // If the keyword signals the end.
            if (model.End.HasValue && model.End.Value) return HandleEndNovelEvent(passage.Label, eventList);
            
            // If a bias is defined.
            if (!string.IsNullOrEmpty(model.Bias))
            {
                string biasString = MapBiasString(model.Bias);
                return HandleBiasEvent(passage, biasString, eventList);
            }
            
            // If a sound is defined.
            if (!string.IsNullOrEmpty(model.Sound))
            {
                return HandlePlaySoundEvent(passage, model.Sound, eventList);
            }
            
            // If it's a character event.
            if (model.CharacterIndex.HasValue)
            {
                string role = GetCharacterRoleFromIndex(model.CharacterIndex.Value, metaData);
                string expression = MapExpressionString(model.FaceExpression);

                // Decide based on the Action field which event to create.
                if (!string.IsNullOrEmpty(model.Action))
                {
                    if (model.Action.Equals("Entry", StringComparison.OrdinalIgnoreCase))
                    {
                        return HandleCharacterJoinsEvent(passage, role, expression, eventList);
                    }

                    if (model.Action.Equals("Speaks", StringComparison.OrdinalIgnoreCase))
                    {
                        return HandleCharacterTalksEvent(passage, role, originalMessage, expression, eventList);
                    }

                    if (model.Action.Equals("Looks", StringComparison.OrdinalIgnoreCase))
                    {
                        // "Looks" is treated as a variant of the join event.
                        return HandleCharacterJoinsEvent(passage, role, expression, eventList);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the bias string (can perform additional mapping if needed).
        /// </summary>
        private static string MapBiasString(string bias)
        {
            // For example, you might want to normalize the bias string.
            if (bias.Equals("AccessToFunding", StringComparison.OrdinalIgnoreCase))
                return "AccessToFunding";
            if (bias.Equals("GenderPayGap", StringComparison.OrdinalIgnoreCase))
                return "GenderPayGap";
            // Add further mappings as needed...
            return bias;
        }

        /// <summary>
        /// Returns the sound string (can perform additional mapping if needed).
        /// </summary>
        //private static string MapSoundString(string sound)
        //{
        //    if (sound.Equals("WaterPouring", StringComparison.OrdinalIgnoreCase))
        //        return "WaterPouring";
        //    if (sound.Equals("LeaveScene", StringComparison.OrdinalIgnoreCase))
        //        return "LeaveScene";
        //    if (sound.Equals("TelephoneCall", StringComparison.OrdinalIgnoreCase))
        //        return "TelephoneCall";
        //    if (sound.Equals("PaperSound", StringComparison.OrdinalIgnoreCase))
        //        return "PaperSound";
        //    if (sound.Equals("ManLaughing", StringComparison.OrdinalIgnoreCase))
        //        return "ManLaughing";
        //    return sound;
        //}

        /// <summary>
        /// Returns the expression string (can perform additional mapping if needed).
        /// </summary>
        private static string MapExpressionString(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return "NeutralRelaxed";
            return expression;
        }

        /// <summary>
        /// Determines the character role as a string based on the CharacterIndex.
        /// 0: "Info", 1: "Player", 2: first talking partner, 3: second talking partner, 4: third talking partner.
        /// </summary>
        private static string GetCharacterRoleFromIndex(int index, KiteNovelMetaData metaData)
        {
            if (index == 0)
                return "Info";
            if (index == 1)
                return "Player";
            if (index == 2)
                return metaData.TalkingPartner01;
            if (index == 3)
                return metaData.TalkingPartner02;
            if (index == 4)
                return metaData.TalkingPartner03;
            return "";
        }

        /// <summary>
        /// Checks if the last created event refers to the start of the novel.
        /// If so, creates a new end event and adjusts the linking to avoid loops.
        /// </summary>
        private static void HandleLoop(VisualNovelEvent lastEvent, string startLabel, List<VisualNovelEvent> eventList)
        {
            if (lastEvent == null || string.IsNullOrEmpty(lastEvent.nextId) || string.IsNullOrEmpty(startLabel) || eventList == null)
            {
                return;
            }
            if (lastEvent.nextId == startLabel)
            {
                string newLabel = "RandomEndNovelString" + _counterForNamingPurpose;
                _counterForNamingPurpose++;
                HandleEndNovelEvent(newLabel, eventList);
                lastEvent.nextId = newLabel;
            }
        }

        /// <summary>
        /// Initializes the event list with start values (e.g. initial location and character join events)
        /// if defined in the metadata.
        /// </summary>
        private static void InitializeKiteNovelEventList(KiteNovelMetaData metaData, List<VisualNovelEvent> eventList, string startLabel)
        {
            string connectionLabel = "initialCharacterJoinsEvent001";
            string id = "initialLocationEvent001";
            string nextId = connectionLabel;
            // Instead of using an enum, we assume metaData.StartLocation is a string.
            //string location = metaData.StartLocation;

            //if (string.IsNullOrEmpty(location))
            //{
            //    Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial location not found!");
            //}

            //VisualNovelEvent initialLocationEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
            //eventList.Add(initialLocationEvent);

            id = connectionLabel;
            nextId = startLabel;
            string character = metaData.TalkingPartner01;
            if (string.IsNullOrEmpty(character))
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial character role not found!");
            }

            string expression = metaData.StartTalkingPartnerEmotion;
            if (string.IsNullOrEmpty(expression))
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial character expression not found!");
            }

            VisualNovelEvent initialCharacterJoinsEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            eventList.Add(initialCharacterJoinsEvent);
        }

        #region Specific Event Handlers

        //private static VisualNovelEvent HandleLocationEvent(TweePassage passage, string location, List<VisualNovelEvent> list)
        //{
        //    string id = passage?.Label;
        //    string nextId = passage?.Links?[0]?.Target;
        //    VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSetBackgroundEvent(id, nextId, location);
        //    list.Add(novelEvent);
        //    return novelEvent;
        //}

        private static VisualNovelEvent HandleCharacterTalksEvent(TweePassage passage, string character, string dialogMessage, string expression, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;

            string nextId = passage?.Links?.FirstOrDefault()?.Target ?? "";
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, character, dialogMessage, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleCharacterJoinsEvent(TweePassage passage, string character, string expression, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleBiasEvent(TweePassage passage, string bias, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetBiasEvent(id, nextId, bias);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleEndNovelEvent(string label, List<VisualNovelEvent> list)
        {
            string label01 = label;
            string label02 = label01 + "RandomString0012003";
            string label03 = label02 + "RandomRandom";
            // Use the sound string directly.
            string leaveSceneSound = "LeaveScene";

            VisualNovelEvent soundEvent = KiteNovelEventFactory.GetPlaySoundEvent(label01, label02, leaveSceneSound);
            VisualNovelEvent exitEvent = KiteNovelEventFactory.GetCharacterExitsEvent(label02, label03);
            VisualNovelEvent endEvent = KiteNovelEventFactory.GetEndNovelEvent(label03);

            list.Add(soundEvent);
            list.Add(exitEvent);
            list.Add(endEvent);

            return null;
        }

        private static void HandleDialogueOptionEvent(TweePassage passage, List<VisualNovelEvent> list, VisualNovelEvent lastEvent)
        {
            if (passage == null || passage.Links == null || passage.Links.Count <= 1)
            {
                return;
            }

            string label = "OptionsLabel" + _counterForNamingPurpose;
            _counterForNamingPurpose++;

            if (lastEvent != null)
            {
                lastEvent.nextId = label;
            }
            else
            {
                label = passage.Label;
            }

            foreach (TweeLink link in passage.Links)
            {
                string id = label;
                label += label;
                string nextId = label;
                string optionText = link.Text;
                string onChoice = link.Target;
                bool showAfterSelection = link.ShowAfterSelection;
                VisualNovelEvent visualNovelEvent = KiteNovelEventFactory.GetAddChoiceEvent(id, nextId, optionText, onChoice, showAfterSelection);
                list.Add(visualNovelEvent);
            }

            VisualNovelEvent showChoicesEvent = KiteNovelEventFactory.GetShowChoicesEvent(label);
            list.Add(showChoicesEvent);
        }

        private static VisualNovelEvent HandlePlaySoundEvent(TweePassage passage, string sound, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlaySoundEvent(id, nextId, sound);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandlePlayAnimationEvent(TweePassage passage, string animation, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetPlayAnimationEvent(id, nextId, animation);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleGptRequestEvent(TweePassage passage, string message, string completionHandlerId, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: GPT Prompt Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string prompt = parts[1].Trim();
            string variableName = parts[0].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetGptEvent(id, nextId, prompt, variableName, completionHandlerId);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSaveDataEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Save Persistent Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSavePersistentEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSetVariableEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Set Variable Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetSaveVariableEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleSetVariableFromBooleanExpressionEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Calculate Variable from Boolean Expression Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCalculateVariableFromBooleanExpressionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackUnderConditionEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string[] parts = message.Split(new[] { ':' }, 2);

            if (parts.Length != 2)
            {
                Debug.LogWarning("While creating Visual Novels: Add Feedback Under Condition Event could not be created.");
                return null;
            }

            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetAddFeedbackUnderConditionEvent(id, nextId, key, value);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleAddFeedbackEvent(TweePassage passage, string message, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;
            string nextId = passage?.Links?[0]?.Target;
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetAddFeedbackEvent(id, nextId, message);
            list.Add(novelEvent);
            return novelEvent;
        }

        #endregion
    }
}
