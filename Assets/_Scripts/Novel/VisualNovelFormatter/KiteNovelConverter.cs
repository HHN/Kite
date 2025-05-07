using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Scripts._Mappings;
using UnityEngine;

namespace Assets._Scripts.Novel.VisualNovelFormatter
{
    #region NovelKeywordModel and Parser

    /// <summary>
    /// Model for a keyword. All fields are optional.
    /// </summary>
    public class NovelKeywordModel
    {
        public int CharacterIndex { get; set; }
        public int FaceExpression { get; set; }
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
        private static NovelKeywordModel ParseKeyword(string keyword, KiteNovelMetaData kiteNovelMetaData)
        {
            // Falls der Eingabestring leer oder nur Whitespace ist, wird null zurückgegeben.
            if (string.IsNullOrWhiteSpace(keyword))
            {
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
                model.CharacterIndex = MappingManager.MapCharacter("Info");
                model.FaceExpression = MappingManager.MapFaceExpressions("NeutralRelaxed");
                return model;
            }

            if (string.Equals(keyword, "Player", StringComparison.OrdinalIgnoreCase))
            {
                model.CharacterIndex = MappingManager.MapCharacter("Player");
                model.FaceExpression = MappingManager.MapFaceExpressions("NeutralRelaxed");
                return model;
            }

            // Teile den String anhand des Trennzeichens '|'.
            string[] parts = keyword.Split('|');
            if (parts.Length > 0)
            {
                // If it is a Character keyword.
                if (parts[0].StartsWith("Character", StringComparison.OrdinalIgnoreCase))
                {
                    // Example: "Character1" → extract the number.
                    string numberPart = parts[0].Substring("Character".Length);
                    if (int.TryParse(numberPart, out int num))
                    {
                        if (num == 1)
                        {
                            model.CharacterIndex = MappingManager.MapCharacter(kiteNovelMetaData.TalkingPartner01);
                        }
                        else if (num == 2)
                        {
                            model.CharacterIndex = MappingManager.MapCharacter(kiteNovelMetaData.TalkingPartner02);
                        }
                        else if (num == 3)
                        {
                            model.CharacterIndex = MappingManager.MapCharacter(kiteNovelMetaData.TalkingPartner03);
                        }
                    }

                    // If there are exactly two parts, then only the face expression is provided.
                    // In this case, we default to "Speaks" as the action.
                    if (parts.Length == 2)
                    {
                        model.FaceExpression = MappingManager.MapFaceExpressions(parts[1]);
                    }
                    // If there are at least three parts, use the provided action and face expression.
                    else if (parts.Length >= 3)
                    {
                        model.FaceExpression = MappingManager.MapFaceExpressions(parts[2]);
                    }
                    // Optional: Falls es nur ein Part gibt (was nicht vorkommen sollte), wird kein Model erstellt.
                    else
                    {
                        Debug.LogWarning("Character keyword does not contain enough parts: " + keyword);
                        return null;
                    }

                    return model;
                }
                // Wenn es sich um ein Sound‑Keyword handelt.
                else if (parts[0].StartsWith("Sound", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length > 1)
                    {
                        model.CharacterIndex = 0;
                        model.Sound = parts[1];
                    }

                    return model;
                }
                // Wenn es sich um ein Bias‑Keyword handelt.
                else if (parts[0].StartsWith("Bias", StringComparison.OrdinalIgnoreCase))
                {
                    if (parts.Length > 1)
                    {
                        // Wende das externe Mapping an.
                        model.Bias = MappingManager.MapBias(parts[1]);
                    }

                    return model;
                }
            }

            // Falls keiner der erwarteten Fälle eintritt, wird null zurückgegeben.
            return null;
        }

        /// <summary>
        /// Parst den kompletten Eingabetext, der mehrere Keyword-Zeilen enthalten kann, 
        /// unter Verwendung des Separators ">>--<<" und gibt eine Liste der gültigen NovelKeywordModel zurück.
        /// </summary>
        /// <param name="fileContent">Der gesamte Text aus der Keyword-Datei.</param>
        /// <returns>Liste der NovelKeywordModel.</returns>
        public static List<NovelKeywordModel> ParseKeywordsFromFile(List<string> fileContent, KiteNovelMetaData kiteNovelMetaData = null)
        {
            // string[] lines = fileContent.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            List<NovelKeywordModel> models = new List<NovelKeywordModel>();

            // Wir gehen davon aus, dass einzelne Keyword-Blöcke durch den Separator ">>--<<" getrennt sind.
            foreach (string line in fileContent)
            {
                // Falls der Separator in der Zeile vorkommt, wird diese Zeile in mehrere Tokens geteilt.
                string[] tokens = line.Split(new string[] { ">>--<<" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string token in tokens)
                {
                    string trimmedToken = token.Trim();
                    if (string.IsNullOrWhiteSpace(trimmedToken))
                        continue;

                    // Stelle sicher, dass die Keywords die Marker ">>" und "<<" haben.
                    if (!(trimmedToken.StartsWith(">>") && trimmedToken.EndsWith("<<")))
                    {
                        continue;
                    }

                    NovelKeywordModel model = ParseKeyword(trimmedToken, kiteNovelMetaData);
                    if (model != null)
                    {
                        models.Add(model);
                    }
                }
            }

            return models;
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
                var characters = new List<string>();

                // Füge nur nicht-leere Namen hinzu
                if (!string.IsNullOrWhiteSpace(folder.NovelMetaData.TalkingPartner01))
                    characters.Add(folder.NovelMetaData.TalkingPartner01);

                if (!string.IsNullOrWhiteSpace(folder.NovelMetaData.TalkingPartner02))
                    characters.Add(folder.NovelMetaData.TalkingPartner02);

                if (!string.IsNullOrWhiteSpace(folder.NovelMetaData.TalkingPartner03))
                    characters.Add(folder.NovelMetaData.TalkingPartner03);
                
                Debug.Log($"folder.NovelMetaData.NovelColor: {folder.NovelMetaData.NovelColor}");

                VisualNovel novel = new VisualNovel
                {
                    id = folder.NovelMetaData.IdNumberOfNovel,
                    title = folder.NovelMetaData.TitleOfNovel,
                    description = folder.NovelMetaData.DescriptionOfNovel,
                    context = folder.NovelMetaData.ContextForPrompt,
                    novelEvents = folder.NovelEventList,
                    characters = characters,
                    isKiteNovel = folder.NovelMetaData.IsKiteNovel,
                    novelColor = ColorUtility.TryParseHtmlString(folder.NovelMetaData.NovelColor, out Color c) ? c : Color.black,
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
                List<string> message = TweeProcessor.ExtractMessageOutOfTweePassage(passage.Passage);

                List<string> keywords = TweeProcessor.ExtractKeywordOutOfTweePassage(passage.Passage);

                // Generate a NovelKeywordModel from the message text.
                List<NovelKeywordModel> keywordModels = NovelKeywordParser.ParseKeywordsFromFile(keywords, metaData);

                if (keywordModels.Count > 1)
                {
                    string targetString = "";
                    int messageIndex = 0;

                    for (int i = 0; i < keywordModels.Count; i++)
                    {
                        VisualNovelEvent createdEvent = new VisualNovelEvent();
                        if (message.Count == 0)
                        {
                            createdEvent = CreateVisualNovelEventFromKeyword(passage, "", keywordModels[i], metaData, eventList);
                        }
                        else if (message.Count <= messageIndex)
                        {
                            createdEvent = CreateVisualNovelEventFromKeyword(passage, message[message.Count - 1], keywordModels[i], metaData, eventList);
                        }
                        else
                        {
                            // Create the corresponding VisualNovelEvent based on the model.
                            createdEvent = CreateVisualNovelEventFromKeyword(passage, message[messageIndex], keywordModels[i], metaData, eventList);
                        }

                        // If there is a link to the next event
                        // We use the label bc it is unique
                        if (targetString == "" && passage.Links.Any())
                        {
                            targetString = passage.Label;
                        }
                        // If it's the last event (no next event after this)
                        else if (targetString == "" && !passage.Links.Any())
                        {
                            targetString = passage.Label + "newTarget";
                        }

                        // First event in list
                        if (i == 0)
                        {
                            createdEvent.nextId = targetString + (i + 1);
                        }
                        // Not the last element
                        else if (i != keywordModels.Count - 1)
                        {
                            createdEvent.id = targetString + i;
                            createdEvent.nextId = targetString + (i + 1);
                        }
                        // Last element
                        else
                        {
                            if (createdEvent == null)
                            {
                                eventList[eventList.Count - 3].id = targetString + (i);
                            }
                            else
                            {
                                createdEvent.id = targetString + i;
                                createdEvent.nextId = passage.Links[0].Target;
                            }
                        }

                        // angenommen createdEvent ist hier schon zugewiesen
                        if (createdEvent == null)
                        {
                            Debug.LogWarning($"No event created for passage {passage.Label}, skipping debug output.");
                        }
                        else
                        {
                            if (createdEvent.eventType == 4)
                            {
                                messageIndex++;
                            }

                            var parts = new List<string>();

                            if (!string.IsNullOrEmpty(createdEvent.id))
                                parts.Add($"id={createdEvent.id}");
                            if (!string.IsNullOrEmpty(createdEvent.nextId))
                                parts.Add($"nextId={createdEvent.nextId}");
                            if (!string.IsNullOrEmpty(createdEvent.onChoice))
                                parts.Add($"onChoice={createdEvent.onChoice}");

                            if (createdEvent.eventType != 0)
                                parts.Add($"eventType={createdEvent.eventType}");
                            if (createdEvent.character != 0)
                                parts.Add($"character={createdEvent.character}");

                            if (!string.IsNullOrEmpty(createdEvent.text))
                                parts.Add($"text=\"{createdEvent.text}\"");

                            if (createdEvent.animationType != 0)
                                parts.Add($"animationType={createdEvent.animationType}");
                            if (createdEvent.expressionType != 0)
                                parts.Add($"expressionType={createdEvent.expressionType}");

                            if (!string.IsNullOrEmpty(createdEvent.key))
                                parts.Add($"key={createdEvent.key}");
                            if (!string.IsNullOrEmpty(createdEvent.value))
                                parts.Add($"value={createdEvent.value}");
                            if (!string.IsNullOrEmpty(createdEvent.relevantBias))
                                parts.Add($"relevantBias={createdEvent.relevantBias}");
                        }


                        // Check if the event creates a loop, and adjust if necessary.
                        HandleLoop(createdEvent, startLabel, eventList);

                        // If dialogue options are present, process them.
                        HandleDialogueOptionEvent(passage, eventList, createdEvent);
                    }
                }
                else if (keywordModels.Count == 1)
                {
                    VisualNovelEvent createdEvent = new VisualNovelEvent();
                    if (message.Count == 0)
                    {
                        createdEvent = CreateVisualNovelEventFromKeyword(passage, "", keywordModels[0], metaData, eventList);
                    }
                    else
                    {
                        // Create the corresponding VisualNovelEvent based on the model.
                        createdEvent = CreateVisualNovelEventFromKeyword(passage, message[0], keywordModels[0], metaData, eventList);
                    }

                    // Check if the event creates a loop, and adjust if necessary.
                    HandleLoop(createdEvent, startLabel, eventList);

                    // If dialogue options are present, process them.
                    HandleDialogueOptionEvent(passage, eventList, createdEvent);
                }
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
            if (model.End.HasValue && model.End.Value)
            {
                HandleEndNovelEvent(passage.Label, eventList);
                return null;
            }

            // If a bias is defined.
            else if (!string.IsNullOrEmpty(model.Bias))
            {
                return HandleBiasEvent(passage, model.Bias, eventList);
            }

            // If a sound is defined.
            else if (!string.IsNullOrEmpty(model.Sound))
            {
                return HandlePlaySoundEvent(passage, model.Sound, eventList);
            }

            // If it's a character event.
            int character = model.CharacterIndex; //GetCharacterRoleFromIndex(model.CharacterIndex, metaData);
            int expression = model.FaceExpression;

            return HandleCharacterTalksEvent(passage, character, originalMessage, expression, eventList);
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
            string connectionLabel = "InitialCharacterJoinsEvent001";
            string id = "initialLocationEvent001";
            string nextId = connectionLabel;

            id = connectionLabel;
            nextId = startLabel;
            int character = MappingManager.MapCharacter(metaData.TalkingPartner01);
            if (character == -1)
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial character role not found!");
            }

            int expression = MappingManager.MapFaceExpressions(metaData.StartTalkingPartnerExpression);
            if (expression == -1)
            {
                Debug.LogWarning("While loading " + metaData.TitleOfNovel + ": Initial character expression " + metaData.StartTalkingPartnerExpression + "not found!");
            }

            VisualNovelEvent initialCharacterJoinsEvent = KiteNovelEventFactory.GetCharacterJoinsEvent(id, nextId, character, expression);
            eventList.Add(initialCharacterJoinsEvent);
        }

        #region Specific Event Handlers

        private static VisualNovelEvent HandleCharacterTalksEvent(TweePassage passage, int character, string dialogMessage, int expression, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;

            string nextId = passage?.Links?.FirstOrDefault()?.Target ?? "";
            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetCharacterTalksEvent(id, nextId, character, dialogMessage, expression);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static VisualNovelEvent HandleBiasEvent(TweePassage passage, string bias, List<VisualNovelEvent> list)
        {
            string id = passage?.Label;

            string nextId = (passage?.Links != null && passage.Links.Count > 0)
                ? passage.Links[0]?.Target ?? ""
                : "";

            VisualNovelEvent novelEvent = KiteNovelEventFactory.GetBiasEvent(id, nextId, bias);
            list.Add(novelEvent);
            return novelEvent;
        }

        private static void HandleEndNovelEvent(string label, List<VisualNovelEvent> list)
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