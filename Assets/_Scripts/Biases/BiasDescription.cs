using UnityEngine;

// Diese Zeile sorgt dafür, dass du neue "Bias Description"-Assets direkt
// über das "Create"-Menü in Unity erstellen kannst.
namespace Assets._Scripts.Biases
{
    [CreateAssetMenu(fileName = "NewBiasDescription", menuName = "Biases/Bias Description")]
    public class BiasDescription : ScriptableObject
    {
        // Hier verknüpfen wir das Daten-Asset mit dem passenden Enum-Typ.
        // Das macht das spätere Nachschlagen im Code sehr einfach.
        public BiasDescriptionTexts.BiasType biasType;

        // Die Überschrift für den Bias.
        public string headline;

        // [TextArea] sorgt für ein größeres, mehrzeiliges Textfeld im Inspector,
        // was die Bearbeitung langer Texte deutlich angenehmer macht.
        [TextArea(5, 10)]
        public string preview;

        [TextArea(10, 20)]
        public string description;
    }
}