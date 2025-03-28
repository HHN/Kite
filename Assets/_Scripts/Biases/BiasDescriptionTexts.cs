using System.Collections.Generic;

namespace Assets._Scripts.Biases
{
    public enum BiasType
    {
        AccessToFinancing,
        GenderPayGap,
        UndervaluationOfWomenLedBusinesses,
        RiskAversionBias,
        ConfirmationBias,
        Tokenism,
        BiasInThePerceptionOfLeadershipSkills,
        BenevolentSexismBias,
        AgeAndGenerationBiases,
        StereotypesTowardsWomenInNonTraditionalIndustries,
        Heteronormativity,
        MaternalBias,
        ExpectationsRegardingFamilyPlanning,
        WorkLifeBalance,
        GenderSpecificStereotypes,
        TightropeBias,
        Microaggressions,
        PerformanceAttributionBias,
        UnconsciousBiasInCommunication
    }
    
    public static class BiasDescriptionTexts
    {
        public static readonly string PLACEHOLDER =
            "<size=30><b>Schwierigkeiten von Frauen, Kapital für ihre Unternehmen zu beschaffen. Hier einige Daten und Fakten dazu:\n\n</b>" +
            "In Deutschland ist es so und so, und hier gibt es Zahlen dazu. Und so weiter und so weiter..." +
            "In Deutschland ist es so und so, und hier gibt es Zahlen dazu. Und so weiter und so weiter..." +
            "In Deutschland ist es so und so, und hier gibt es Zahlen dazu. Und so weiter und so weiter..." +
            "In Deutschland ist es so und so, und hier gibt es Zahlen dazu. Und so weiter und so weiter..." +
            "In Deutschland ist es so und so, und hier gibt es Zahlen dazu. Und so weiter und so weiter...\n\n</size>";
        
        private static readonly string LinkColor = "#F5944E";

        #region AccessToFinancing

        private static readonly string FinanzierungszugangHeadline = "AccessToFinancing";

        private static readonly string FinanzierungszugangDescriptionPreview =
            "Kapital ist der Schlüssel zum Unternehmenswachstum, doch Gründerinnen erhalten deutlich weniger Zugang " +
            "zu Finanzierung als ihre männlichen Kollegen. Nur 2 % des gesamten Wagniskapitals in Deutschland" +
            "fließen in rein weibliche Teams – eine markante Diskrepanz.\n\n";

        private static readonly string FinanzierungszugangDescription =
            $"Studien zeigen, dass Investorinnen und Investoren Frauen oft als risikoaverser wahrnehmen und ihre " +
            $"Geschäftsmodelle kritischer hinterfragen. Während männliche Gründer häufiger zu Wachstumsstrategien " +
            $"befragt werden, müssen Frauen verstärkt ihre Risikominimierung erklären. Das führt dazu, dass sie " +
            $"seltener an große Finanzierungsrunden kommen und langsamer skalieren können " +
            $"(<color={LinkColor}><link=\"https://www.ey.com/de_de/newsroom/2024/02/startup-barometer-gruenderinnen\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Familienmitglied skeptisch fragt, ob genügend Kapital " +
            $"vorhanden ist, um das Unternehmen langfristig zu finanzieren. Während männlichen Gründern oft automatisch " +
            $"zugetraut wird, die Finanzierung bereits gesichert zu haben, müssen Frauen erst beweisen, " +
            $"dass sie Kapital beschaffen können.";

        #endregion

        #region Gender Pay Gap

        private static readonly string GenderPayGapHeadline = "Gender Pay Gap";

        private static readonly string GenderPayGapDescriptionPreview =
            "Die geschlechtsspezifische Lohnlücke betrifft nicht nur Angestellte – auch Gründerinnen sind davon " +
            "betroffen. In Deutschland erhalten Startups mit rein weiblichen Teams pro Finanzierungsrunde " +
            "durchschnittlich nur ein Drittel der Summe, die männlich geführte Startups erhalten.\n\n";

        private static readonly string GenderPayGapDescription =
            $"Das bedeutet, dass Frauen mit weniger Kapital starten, langsamer wachsen und es schwerer haben, " +
            $"Investorinnen und Investoren von der Rentabilität ihres Unternehmens zu überzeugen. " +
            $"Viele Investorinnen und Investoren gehen davon aus, dass Frauen vorsichtiger kalkulieren oder weniger " +
            $"ambitioniert in Verhandlungen sind – eine Annahme, die durch Studien nicht gestützt wird, aber dennoch " +
            $"Finanzierungsentscheidungen beeinflusst " +
            $"(<color={LinkColor}><link=\"https://web-assets.bcg.com/img-src/20190910_BCG_PM_Diversity%20in%20Start-Ups_fwi_AG_tcm9-228682.pdf\">" +
            $"Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich das, wenn eine Verhandlungspartnerin erwartet, dass eine Gründerin " +
            $"flexibler beim Honorar ist. Die Annahme, dass weibliche Unternehmerinnen niedrigere Preise akzeptieren " +
            $"sollten, zeigt, wie tief verwurzelt diese finanzielle Abwertung ist.";

        #endregion

        #region Unterbewertung weiblich geführter Unternehmen

        private static readonly string UnterbewertungWeiblichGefuehrterUnternehmenHeadline =
            "Unterbewertung weiblich geführter Unternehmen";

        private static readonly string UnterbewertungWeiblichGefuehrterUnternehmenPreview =
            "Gründerinnen werden systematisch niedriger bewertet als männliche Gründer – selbst wenn ihre Unternehmen " +
            "ähnliche oder bessere Wachstumszahlen haben. Eine Studie zeigt, dass der Wert von deutschen " +
            "Neugründungen mit Männern an der Spitze durchschnittlich 16,4-mal höher eingeschätzt wird.\n\n";

        private static readonly string UnterbewertungWeiblichGefuehrterUnternehmenDescription =
            $"Dies liegt jedoch nicht an mangelndem Selbstbewusstsein, sondern daran, dass Investorinnen und " +
            $"Investoren Frauen häufig geringere Skalierungsmöglichkeiten und weniger Führungsstärke zutrauen. " +
            $"Dadurch sind Gründerinnen gezwungen, konservativer zu kalkulieren, was ihre Finanzierungsrunden " +
            $"zusätzlich erschwert. Auch in Gesprächen mit Geschäftspartnerinnen und Geschäftspartnern werden " +
            $"weibliche Gründer öfter skeptischer hinterfragt als ihre männlichen Kollegen " +
            $"(<color={LinkColor}><link=\"https://web-assets.bcg.com/img-src/20190910_BCG_PM_Diversity%20in%20Start-Ups_fwi_AG_tcm9-228682.pdf\">" +
            $"Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich das, wenn ein Investor die Unternehmensbewertung als „ambitioniert“ für " +
            $"eine Gründerin einstuft – unabhängig von der Marktanalyse und realen Erfolgszahlen. Diese unterschwellige " +
            $"Abwertung führt dazu, dass Frauen für ihre Unternehmen oft schlechtere Konditionen erhalten.";

        #endregion

        #region Risk Aversion Bias

        private static readonly string RiskAversionBiasHeadline =
            "Risk Aversion Bias";

        private static readonly string RiskAversionBiasPreview =
            "Frauen wird oft unterstellt, dass sie weniger risikobereit sind als Männer. Tatsächlich zeigen Studien, " +
            "dass Gründerinnen Risiken bewusster analysieren und detaillierter abwägen – doch genau diese Strategie " +
            "wird ihnen negativ ausgelegt. Während männliche Gründer für ihre „besonnene“ Planung gelobt werden, " +
            "gelten Frauen bei gleichem Verhalten als „zögerlich“ oder „übervorsichtig“.\n\n";

        private static readonly string RiskAversionBiasDescription =
            $"Diese Wahrnehmung führt dazu, dass Investorinnen und Investoren Frauen seltener als dynamische " +
            $"Unternehmerinnen wahrnehmen und ihnen weniger Kapital für Wachstum und Expansion anvertrauen. " +
            $"Dadurch entsteht ein Teufelskreis: Weniger Kapital bedeutet langsamere Skalierung, was wiederum die " +
            $"bestehenden Vorurteile über zu vorsichtige Gründerinnen zu bestätigen scheint " +
            $"(<color={LinkColor}><link=\"https://www.kfw.de/%C3%9Cber-die-KfW/KfW-Research/Female-Entrepreneurship.html\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Investor bezweifelt, dass Frauen wirklich bereit " +
            $"sind, aggressiv genug zu skalieren – als wäre Risikobereitschaft die einzige Strategie für Wachstum. " +
            $"Auch ein Vermieter stellt die Belastbarkeit infrage, obwohl die Geschäftszahlen eine durchdachte und " +
            $"erfolgversprechende Strategie belegen.";

        #endregion

        #region Bestätigungsverzerrung (Confirmation Bias)

        private static readonly string ConfirmationBiasHeadline =
            "Bestätigungsverzerrung (Confirmation Bias)";

        private static readonly string ConfirmationBiasPreview =
            "Menschen nehmen Informationen so wahr, dass sie ihre bestehenden Überzeugungen bestätigen. " +
            "Genau das geschieht bei Gründerinnen: Investorinnen und Investoren stellen Frauen häufiger Fragen zur " +
            "Risikominimierung, während sie bei männlichen Gründern nach Wachstumsstrategien fragen. Das beeinflusst " +
            "nicht nur die Wahrnehmung der Unternehmerin, sondern auch ihre Chancen auf Finanzierung.\n\n";

        private static readonly string ConfirmationBiasDescription =
            $"Frauen werden in Pitches subtil anders behandelt, ihre Pläne kritischer hinterfragt und ihre Ideen " +
            $"mit einer höheren Skepsis betrachtet. Dadurch verstärkt sich der Eindruck, dass weibliche Gründungsteams " +
            $"vorsichtiger und weniger wachstumsorientiert sind – ein klassischer Fall von sich selbst erfüllender " +
            $"Prophezeiung (<color={LinkColor}><link=\"https://www.kfw.de/%C3%9Cber-die-KfW/KfW-Research/Female-Entrepreneurship.html\">" +
            $"Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Bank-Mitarbeiter seine stereotype Wahrnehmung von " +
            $"Gründerinnen mit „jahrelanger Erfahrung“ rechtfertigt. Anstatt sich auf Zahlen und Fakten zu stützen, " +
            $"bleibt er bei seinen subjektiven Annahmen und hinterfragt eine Gründerin strenger als einen männlichen " +
            $"Gründer mit der gleichen Geschäftsidee.";

        #endregion

        #region Tokenism

        private static readonly string TokenismHeadline =
            "Tokenism";

        private static readonly string TokenismPreview =
            "Wenn Frauen in männerdominierten Branchen erfolgreich sind, werden sie oft als „Vorzeigebeispiele“ " +
            "für Diversität genutzt – allerdings ohne ihnen die gleiche Entscheidungs- und Gestaltungsmacht " +
            "zuzugestehen. Häufig erscheinen sie in Werbematerialien oder auf Veranstaltungen als Aushängeschild " +
            "für Inklusion, während die tatsächliche Kontrolle über strategische Entscheidungen weiterhin bei " +
            "männlichen Kollegen liegt.\n\n";

        private static readonly string TokenismDescription =
            $"Dieses Phänomen wird als Tokenism bezeichnet: Es vermittelt den Anschein von Fortschritt, " +
            $"führt aber in der Praxis nicht zu strukturellen Veränderungen. Studien zeigen, dass Frauen in solchen " +
            $"Situationen oft auf ihre Identität als Frau reduziert werden, anstatt für ihre unternehmerische Leistung " +
            $"anerkannt zu werden. Das kann dazu führen, dass Gründerinnen weniger ernst genommen werden oder in " +
            $"Verhandlungen weniger Einfluss erhalten " +
            $"(<color={LinkColor}><link=\"https://www.tandfonline.com/doi/full/10.1080/08985626.2018.1551795\">" +
            $"Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn eine Journalistin Dein Startup nicht wegen seiner " +
            $"Innovation oder seines Geschäftsmodells spannend findet, sondern einzig, weil es von einer Frau " +
            $"gegründet wurde. Die Gründerin wird nicht als erfolgreiche Unternehmerin wahrgenommen, sondern " +
            $"als „Ausnahmeerscheinung“, was ihre tatsächlichen unternehmerischen Leistungen in den Hintergrund rückt.";

        #endregion

        #region Bias in der Wahrnehmung von Führungsfähigkeiten

        private static readonly string BiasInDerWahrnehmungVonFuehrungsfaehigkeitenHeadline =
            "Bias in der Wahrnehmung von Führungsfähigkeiten";

        private static readonly string BiasInDerWahrnehmungVonFuehrungsfaehigkeitenPreview =
            "Führung wird häufig mit Eigenschaften assoziiert, die traditionell als „männlich“ gelten – wie " +
            "Durchsetzungsstärke, Dominanz oder Entscheidungsfreude. Frauen in Führungspositionen stehen daher " +
            "unter besonderer Beobachtung: Ihre Kompetenz wird kritischer beurteilt, sie müssen sich häufiger " +
            "beweisen und werden schneller infrage gestellt.\n\n";

        private static readonly string BiasInDerWahrnehmungVonFuehrungsfaehigkeitenDescription =
            $"Die verbreitete Denkweise „think manager, think male“ oder „think entrepreneur, think male“ sorgt dafür, " +
            $"dass Frauen in der Unternehmenswelt oft nicht als natürliche Führungspersönlichkeiten wahrgenommen " +
            $"werden (<color={LinkColor}><link=\"https://link.springer.com/article/10.1007/s11365-018-0553-0\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, als eine Notarin Dich nach dem Geschäftsführer fragt und " +
            $"selbstverständlich annimmt, dass es sich um einen Mann handeln muss. Die unausgesprochene Annahme: " +
            $"Unternehmerische Führung liegt nicht in weiblicher Hand. Solche Denkmuster verstärken strukturelle Hürden " +
            $"für Gründerinnen, die nicht nur ihre Qualifikation beweisen, sondern auch bestehende Vorurteile überwinden müssen.";

        #endregion

        #region Benevolenter Sexismus

        private static readonly string BenevolenterSexismusHeadline =
            "Benevolenter Sexismus";

        private static readonly string BenevolenterSexismusPreview =
            "Manchmal tarnt sich Diskriminierung als vermeintliches Lob. Wohlwollender Sexismus klingt positiv, " +
            "ist aber herablassend: Frauen werden für ihre „besondere Sensibilität“ oder „weibliche Intuition“ " +
            "gelobt, während ihnen gleichzeitig wirtschaftliche Fähigkeiten abgesprochen werden. Diese Art von " +
            "Vorurteil verstärkt subtile Schranken, indem sie Frauen sanfte, unterstützende Rollen zuweist, anstatt " +
            "sie als gleichwertige Akteure in der Wirtschaft zu sehen.\n\n";

        private static readonly string BenevolenterSexismusDescription =
            $"Eine aktuelle Studie zeigt, dass Investorinnen und Investoren mit einer hohen Ausprägung an " +
            $"benevolentem Sexismus männlich geführte Startups positiver bewerten, während sie Frauen nicht " +
            $"zwingend abwerten – sondern stattdessen Männer gezielt bevorzugen " +
            $"(<color={LinkColor}><link=\"https://journals.sagepub.com/doi/full/10.1177/10422587231178865\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Vermieter Deine „Frauenpower“ lobt – aber " +
            $"gleichzeitig andeutet, dass Unternehmertum für Frauen „sehr stressig“ sei. Die Botschaft dahinter: " +
            $"Frauen sind zwar bewundernswert, aber möglicherweise „besser aufgehoben“ in weniger belastenden Berufen. " +
            $"Solche Aussagen klingen unterstützend, bewirken aber das Gegenteil, indem sie Frauen subtil aus " +
            $"anspruchsvollen Karrierewegen herausdrängen.";

        #endregion

        #region Alters- und Generationen-Biases

        private static readonly string AltersUndGenerationenBiasHeadline =
            "Alters- und Generationen-Biases";

        private static readonly string AltersUndGenerationenBiasPreview =
            "Frauen werden je nach Alter unterschiedlich benachteiligt – sowohl junge als auch ältere Gründerinnen " +
            "stehen vor unsichtbaren Hürden. Während jüngeren Frauen oft die nötige Erfahrung oder Ernsthaftigkeit " +
            "abgesprochen wird, werden ältere Gründerinnen seltener als dynamisch oder innovativ wahrgenommen.\n\n";

        private static readonly string AltersUndGenerationenBiasDescription =
            $"Dabei zeigt ein genauer Blick auf die Zahlen: Das Bild vom jugendlichen Garagengründer ist ein Mythos. " +
            $"Tatsächlich ist das Gründungsgeschehen viel altersdiverser, doch die Förder- und Startup-Landschaft " +
            $"bildet dies nicht ausreichend ab. Ältere Gründerinnen und Gründer erhalten seltener Zugang zu Kapital " +
            $"und Förderprogrammen, obwohl sie über mehr berufliche Erfahrung und Netzwerke verfügen " +
            $"(<color={LinkColor}><link=\"https://fosteringinnovation.de/gruenden-in-der-mitte-des-lebens-das-potenzial-aelterer-entrepreneure/\">" +
            $"Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Vermieter misstrauisch fragt, ob Deine Gründung " +
            $"wirklich ernst gemeint ist oder nur ein „Hobbyprojekt“. Solche Vorurteile betreffen besonders ältere " +
            $"Gründerinnen, da ihnen oft unterstellt wird, nicht die Energie oder den langfristigen Antrieb für " +
            $"ein wachstumsstarkes Unternehmen zu haben.";

        #endregion

        #region Stereotype gegenüber Frauen in nicht-traditionellen Branchen

        private static readonly string StereotypeHeadline =
            "Stereotype gegenüber Frauen in nicht-traditionellen Branchen";

        private static readonly string StereotypePreview =
            "Frauen in männerdominierten Branchen stehen oft unter besonderer Beobachtung. Ihnen wird seltener " +
            "Fachkompetenz zugesprochen, selbst wenn sie über die gleichen Qualifikationen verfügen wie ihre " +
            "männlichen Kollegen. Dies ist ein klassischer Fall des Prove-it-Again-Bias: Frauen müssen ihre Expertise " +
            "immer wieder unter Beweis stellen, während Männern ihre Kompetenz eher automatisch zugetraut wird.\n\n";

        private static readonly string StereotypeDescription =
            $"Studien zeigen, dass Frauen in Technik-, Bau- und Ingenieurberufen häufiger hinterfragt werden und " +
            $"seltener als Experten wahrgenommen werden. Diese strukturellen Vorurteile führen dazu, dass Frauen es " +
            $"schwerer haben, sich am Markt zu behaupten – nicht aufgrund mangelnder Fähigkeiten, sondern weil ihnen " +
            $"weniger Vertrauen entgegengebracht wird " +
            $"(<color={LinkColor}><link=\"https://www.existenzgruendungsportal.de/SharedDocs/Downloads/DE/Publikationen/female-founders-digitalbranche-2020.html\">" +
            $"Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Verhandlungspartner Dir rät, Deine Preise als Frau " +
            $"in der Branche „anzupassen“, um konkurrenzfähig zu bleiben. Die Annahme dahinter: Frauen können sich " +
            $"in bestimmten Märkten nur über Rabatte und niedrigere Preise behaupten – nicht durch Qualität und Kompetenz.";

        #endregion

        #region Heteronormativität

        private static readonly string HeteronormativitaetHeadline =
            "Heteronormativität";

        private static readonly string HeteronormativitaetPreview =
            "Geschäftsbeziehungen sind oft von unausgesprochenen Annahmen über Geschlechterrollen geprägt. " +
            "Frauen wird häufig unterstellt, dass sie in einer heterosexuellen Beziehung leben und dass ihre " +
            "beruflichen Entscheidungen von ihrem Ehemann beeinflusst werden.\n\n";

        private static readonly string HeteronormativitaetDescription =
            $"Besonders für queere Gründerinnen kann das belastend sein, da sie entweder gezwungen sind, ihre " +
            $"Identität offenzulegen oder falsche Annahmen stehenzulassen, um keine geschäftlichen Nachteile zu riskieren " +
            $"(<color={LinkColor}><link=\"https://www.emerald.com/insight/content/doi/10.1108/ijebr-12-2022-1114/full/html\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Bank-Mitarbeiter fragt, was Dein Ehemann zu Deiner " +
            $"Gründung sagt – anstatt Dich als eigenständige Unternehmerin zu sehen. Die implizite Botschaft: Eine " +
            $"Frau gründet nicht alleine, sondern immer im Kontext einer (heterosexuellen) Partnerschaft.";

        #endregion

        #region Maternal Bias

        private static readonly string MaternalBiasHeadline =
            "Maternal Bias";

        private static readonly string MaternalBiasPreview =
            "Mütter werden in der Geschäftswelt oft als weniger engagiert oder weniger belastbar wahrgenommen. " +
            "Besonders Gründerinnen mit Kindern begegnen in Finanzierungsrunden und Geschäftsgesprächen einer " +
            "unausgesprochenen Skepsis: Werden sie sich wirklich voll auf ihr Unternehmen konzentrieren? Diese " +
            "Annahme führt dazu, dass Frauen mit Kindern bei Investoren seltener als erfolgsorientiert oder " +
            "wachstumsstark wahrgenommen werden.\n\n";

        private static readonly string MaternalBiasDescription =
            $"Studien zeigen, dass Gründerinnen in Pitches häufiger Fragen zur Vereinbarkeit von Familie und " +
            $"Beruf gestellt bekommen – eine Frage, die männliche Gründer mit Kindern deutlich seltener hören " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1177/0018726708098082\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Familienmitglied betont, wie schwer es sei, " +
            $"Kinder und Unternehmen unter einen Hut zu bringen – als ob Männer diesen Konflikt nicht hätten. " +
            $"Die unausgesprochene Botschaft: Eine Mutter kann entweder fürsorglich oder erfolgreich sein, " +
            $"aber nicht beides.";

        #endregion

        #region Erwartungshaltung bezüglich Familienplanung

        private static readonly string FamilienplanungBiasHeadline =
            "Erwartungshaltung bezüglich Familienplanung";

        private static readonly string FamilienplanungPreview =
            "Frauen im gebärfähigen Alter werden oft danach beurteilt, ob sie in Zukunft Kinder bekommen könnten – " +
            "selbst wenn sie keinerlei Hinweise darauf geben. Arbeitgeberinnen und Arbeitgeber, Investorinnen und " +
            "Investoren oder Geschäftspartnerinnen und Geschäftspartner treffen unbewusst Annahmen darüber, dass " +
            "Frauen ihre Karriere möglicherweise unterbrechen oder sich weniger auf ihr Unternehmen konzentrieren " +
            "könnten.\n\n";

        private static readonly string FamilienplanungDescription =
            $"Dies führt dazu, dass Gründerinnen bei Finanzierungsverhandlungen oder langfristigen " +
            $"Geschäftspartnerschaften benachteiligt werden " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1016/j.jbusvent.2018.04.003\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Vermieter beiläufig andeutet, dass „jüngere " +
            $"Frauen ja oft von einem Projekt zum nächsten wechseln“ und dann „vielleicht doch irgendwann die " +
            $"Familienplanung ansteht“. Die Unterstellung: Deine unternehmerische Zukunft sei ungewiss, weil sie " +
            $"potenziell von privaten Entscheidungen abhängt.";

        #endregion

        #region Work-Life-Balance-Erwartungen

        private static readonly string WorkLifeBalanceHeadline =
            "Work-Life-Balance-Erwartungen";

        private static readonly string WorkLifeBalancePreview =
            "Frauen stehen häufig unter dem Druck, Beruf und Privatleben in ein „perfektes Gleichgewicht“ zu bringen " +
            "– ein Anspruch, der an Männer in Führungspositionen deutlich seltener gestellt wird. Besonders " +
            "Gründerinnen wird oft unterstellt, dass sie ihr Unternehmen nicht mit anderen Verpflichtungen " +
            "vereinbaren können.\n\n";

        private static readonly string WorkLifeBalanceDescription =
            $"In Deutschland leisten Frauen im Durchschnitt 30 Stunden pro Woche unbezahlte Arbeit, während Männer " +
            $"nur 21 Stunden für solche Tätigkeiten aufbringen. Dieses Ungleichgewicht führt dazu, dass Frauen sich " +
            $"häufiger mit Fragen zu Vereinbarkeit, Belastbarkeit oder Verantwortung konfrontiert sehen, die " +
            $"männliche Gründer seltener hören " +
            $"(<color={LinkColor}><link=\"https://www.destatis.de/DE/Presse/Pressemitteilungen/2024/02/PD24_073_63991.html\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Familienmitglied erzählt, dass ein befreundeter " +
            $"Gründer „extrem viel Stress hatte“ und sich fragt, ob das für Dich nicht auch „zu anstrengend“ wird. " +
            $"Die Annahme dahinter: Frauen sollten automatisch darauf achten, dass sie eine gesunde Balance halten – " +
            $"während Männern unternehmerische Überarbeitung oft als Engagement ausgelegt wird.";

        #endregion

        #region Geschlechtsspezifische Stereotype

        private static readonly string GeschlechtsspezifischeStereotypeHeadline =
            "Geschlechtsspezifische Stereotype";

        private static readonly string GeschlechtsspezifischeStereotypePreview =
            "Stereotype beeinflussen, wie Frauen in der Geschäftswelt wahrgenommen werden. Während Männern " +
            "Eigenschaften wie „analytisch“, „strategisch“ oder „durchsetzungsstark“ zugeschrieben werden, " +
            "werden Frauen oft als „kommunikativ“, „detailliert“ oder „harmonieorientiert“ beschrieben.\n\n";

        private static readonly string GeschlechtsspezifischeStereotypeDescription =
            $"Diese Zuschreibungen sind nicht nur wertend, sondern beeinflussen auch Entscheidungen in der " +
            $"Geschäftswelt – von Einstellungsprozessen über Finanzierungsentscheidungen bis hin zur Wahrnehmung von " +
            $"Führungskompetenz " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1515/erj-2022-0235\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Vermieter beiläufig bemerkt, dass ihm auffällt, " +
            $"dass „Frauen sich detaillierter mit Anforderungen auseinandersetzen“. Auch wenn es positiv klingt, " +
            $"verstärkt es das Stereotyp, dass Frauen eher auf Details achten als auf strategische Unternehmensführung.";

        #endregion

        #region Tightrope Bias

        private static readonly string TightropeBiasHeadline =
            "Tightrope Bias";

        private static readonly string TightropeBiasPreview =
            "Frauen in Führungspositionen bewegen sich oft auf einem schmalen Grat: Wenn sie zu freundlich und " +
            "kooperativ sind, gelten sie als „zu weich“ oder „nicht durchsetzungsfähig“. Sind sie hingegen direkt " +
            "und entscheidungsstark, werden sie als „zu fordernd“ oder „unsympathisch“ wahrgenommen.\n\n";

        private static readonly string TightropeBiasDescription =
            $"Dieser sogenannte Tightrope Bias zwingt Frauen dazu, ständig ihr Verhalten zu balancieren, während " +
            $"männliche Kollegen für dasselbe Verhalten als „entschlossen“ oder „visionär“ wahrgenommen werden " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1002/ltl.20654\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn eine Verhandlungspartnerin betont, dass sie " +
            $"Dein Selbstbewusstsein schätzt – aber gleichzeitig andeutet, dass dieses „zu forsch“ für manche " +
            $"Investorinnen und Investoren wirken könnte. Die implizite Erwartung: Frauen sollten selbstbewusst, " +
            $"aber nicht zu selbstbewusst sein.";

        #endregion

        #region Mikroaggressionen

        private static readonly string MikroaggressionenHeadline =
            "Mikroaggressionen";

        private static readonly string MikroaggressionenPreview =
            "Mikroaggressionen sind subtile, oft unbewusste Bemerkungen oder Gesten, die Frauen in der Geschäftswelt " +
            "abwerten. Sie äußern sich durch beiläufige Kommentare, skeptische Fragen oder nonverbale Signale, " +
            "die Frauen das Gefühl geben, nicht vollständig ernst genommen zu werden.\n\n";

        private static readonly string MikroaggressionenDescription =
            $"Mikroaggressionen summieren sich über die Zeit und können das Selbstbewusstsein von Frauen in " +
            $"Verhandlungen oder Finanzierungsrunden beeinflussen " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1007/978-3-031-50164-7\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Investor zögert und sagt, er frage sich, " +
            $"„ob sich Investoren mit einer weiblichen Geschäftsführung wohlfühlen“. Die Aussage ist nicht direkt " +
            $"abwertend, setzt aber unterschwellig voraus, dass Dein Geschlecht für Deine geschäftlichen " +
            $"Erfolgsaussichten eine Rolle spielt.";

        #endregion

        #region Leistungsattributions-Bias

        private static readonly string LeistungsattributionsBiasHeadline =
            "Leistungsattributions-Bias";

        private static readonly string LeistungsattributionsBiasPreview =
            "Frauen müssen ihren Erfolg häufiger rechtfertigen als Männer. Ihre Leistungen werden oft auf Glück, " +
            "äußere Umstände oder die Unterstützung anderer zurückgeführt, während Männern für die gleichen Erfolge " +
            "strategisches Geschick und Kompetenz zugeschrieben wird. Forschungen zeigen, dass Frauen bei „männlich " +
            "geprägten“ Aufgaben selbst bei objektiv hoher Leistung als weniger kompetent wahrgenommen werden als " +
            "Männer mit identischen Ergebnissen.\n\n";

        private static readonly string LeistungsattributionsBiasDescription =
            $"Dies geschieht durch einen unbewussten Mechanismus, der die Begründung rationalisiert: Beobachtende " +
            $"suchen nach alternativen Erklärungen für die Leistung einer Frau, beispielsweise durch Glück oder die " +
            $"Anwesenheit eines männlichen Kollegen. Diese systematische Abwertung kann dazu führen, dass Frauen sich " +
            $"immer wieder neu beweisen müssen, während Männer schneller als fähig anerkannt werden " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1287/orsc.2022.1594\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn eine Journalistin darauf verweist, dass Du „Glück gehabt " +
            $"hast, genau die richtige Nische zu finden“. Die Botschaft: Dein Erfolg basiert nicht auf Können " +
            $"oder Marktverständnis, sondern auf Zufall – ein klassisches Beispiel für die Abwertung " +
            $"weiblicher Leistungen.";

        #endregion

        #region Unbewusste Bias in der Kommunikation

        private static readonly string UnbewussteBiasInDerKommunikationHeadline =
            "Unbewusste Bias in der Kommunikation";

        private static readonly string UnbewussteBiasInDerKommunikationPreview =
            "Sprache formt Wahrnehmung – und unbewusste Vorurteile spiegeln sich oft in der Art, wie mit Frauen in " +
            "der Geschäftswelt gesprochen wird. Studien zeigen, dass Gründerinnen in Meetings und Finanzierungsrunden " +
            "häufiger unterbrochen, herablassend behandelt oder mit unterschwelligen Zweifeln an ihrer " +
            "Führungsfähigkeit konfrontiert werden. \n\n";

        private static readonly string UnbewussteBiasInDerKommunikationDescription =
            $"Ihnen wird geraten, ihre Ideen „einfacher“ oder „verständlicher“ zu erklären, selbst wenn ihre " +
            $"Präsentationen auf demselben fachlichen Niveau sind wie die ihrer männlichen Kollegen. Ein weiteres " +
            $"Beispiel ist die Abwertung weiblicher Führungsteams durch stereotype Annahmen über weibliche Dynamiken – " +
            $"etwa, dass Frauen in Konflikte oder Klatsch verwickelt seien, anstatt strategische Entscheidungen zu " +
            $"treffen. Solche unbewussten Biases untergraben die Professionalität von Frauen und beeinflussen, wie " +
            $"sie in der Geschäftswelt wahrgenommen werden " +
            $"(<color={LinkColor}><link=\"https://doi.org/10.1007/s10869-022-09871-7\">Quelle</link></color>).\n\n" +
            $"In unseren Stories zeigt sich dieser Bias, wenn ein Bank-Mitarbeiter behauptet, dass es mit mehreren " +
            $"Frauen in der Geschäftsführung schwierig sei – er „denke da an möglichen Zickenkrieg und viel Klatsch " +
            $"und Tratsch“. Diese Form herablassender Kommunikation stellt nicht nur ihre Kompetenz infrage, " +
            $"sondern verstärkt stereotype Rollenbilder.";

        #endregion

        private static readonly Dictionary<BiasType, string[]> BiasDetailsTexts = new()
        {
            {
                BiasType.AccessToFinancing,
                new[]
                {
                    FinanzierungszugangHeadline,
                    FinanzierungszugangDescriptionPreview,
                    FinanzierungszugangDescription
                }
            },
            {
                BiasType.GenderPayGap,
                new[]
                {
                    GenderPayGapHeadline,
                    GenderPayGapDescriptionPreview,
                    GenderPayGapDescription
                }
            },
            {
                BiasType.UndervaluationOfWomenLedBusinesses,
                new[]
                {
                    UnterbewertungWeiblichGefuehrterUnternehmenHeadline,
                    UnterbewertungWeiblichGefuehrterUnternehmenPreview,
                    UnterbewertungWeiblichGefuehrterUnternehmenDescription
                }
            },
            {
                BiasType.RiskAversionBias,
                new[]
                {
                    RiskAversionBiasHeadline,
                    RiskAversionBiasPreview,
                    RiskAversionBiasDescription
                }
            },
            {
                BiasType.ConfirmationBias,
                new[]
                {
                    ConfirmationBiasHeadline,
                    ConfirmationBiasPreview,
                    ConfirmationBiasDescription
                }
            },
            {
                BiasType.Tokenism,
                new[]
                {
                    TokenismHeadline,
                    TokenismPreview,
                    TokenismDescription
                }
            },
            {
                BiasType.BiasInThePerceptionOfLeadershipSkills,
                new[]
                {
                    BiasInDerWahrnehmungVonFuehrungsfaehigkeitenHeadline,
                    BiasInDerWahrnehmungVonFuehrungsfaehigkeitenPreview,
                    BiasInDerWahrnehmungVonFuehrungsfaehigkeitenDescription
                }
            },
            {
                BiasType.BenevolentSexismBias,
                new[]
                {
                    BenevolenterSexismusHeadline,
                    BenevolenterSexismusPreview,
                    BenevolenterSexismusDescription
                }
            },
            {
                BiasType.AgeAndGenerationBiases,
                new[]
                {
                    AltersUndGenerationenBiasHeadline,
                    AltersUndGenerationenBiasPreview,
                    AltersUndGenerationenBiasDescription
                }
            },
            {
                BiasType.StereotypesTowardsWomenInNonTraditionalIndustries,
                new[]
                {
                    StereotypeHeadline,
                    StereotypePreview,
                    StereotypeDescription
                }
            },
            {
                BiasType.Heteronormativity,
                new[]
                {
                    HeteronormativitaetHeadline,
                    HeteronormativitaetPreview,
                    HeteronormativitaetDescription
                }
            },
            {
                BiasType.MaternalBias,
                new[]
                {
                    MaternalBiasHeadline,
                    MaternalBiasPreview,
                    MaternalBiasDescription
                }
            },
            {
                BiasType.ExpectationsRegardingFamilyPlanning,
                new[]
                {
                    FamilienplanungBiasHeadline,
                    FamilienplanungPreview,
                    FamilienplanungDescription
                }
            },
            {
                BiasType.WorkLifeBalance,
                new[]
                {
                    WorkLifeBalanceHeadline,
                    WorkLifeBalancePreview,
                    WorkLifeBalanceDescription
                }
            },
            {
                BiasType.GenderSpecificStereotypes,
                new[]
                {
                    GeschlechtsspezifischeStereotypeHeadline,
                    GeschlechtsspezifischeStereotypePreview,
                    GeschlechtsspezifischeStereotypeDescription
                }
            },
            {
                BiasType.TightropeBias,
                new[]
                {
                    TightropeBiasHeadline,
                    TightropeBiasPreview,
                    TightropeBiasDescription
                }
            },
            {
                BiasType.Microaggressions,
                new[]
                {
                    MikroaggressionenHeadline,
                    MikroaggressionenPreview,
                    MikroaggressionenDescription
                }
            },
            {
                BiasType.PerformanceAttributionBias,
                new[]
                {
                    LeistungsattributionsBiasHeadline,
                    LeistungsattributionsBiasPreview,
                    LeistungsattributionsBiasDescription
                }
            },
            {
                BiasType.UnconsciousBiasInCommunication,
                new[]
                {
                    UnbewussteBiasInDerKommunikationHeadline,
                    UnbewussteBiasInDerKommunikationPreview,
                    UnbewussteBiasInDerKommunikationDescription
                }
            }
        };

        public static string GetBiasText(BiasType biasType)
        {
            if (BiasDetailsTexts.TryGetValue(biasType, out var biasInfo))
            {
                return $"<size=40><align=center><b>{biasInfo[0]}</b></align></size>\n\n" +
                       $"<size=35>{biasInfo[1]}</size>" +
                       $"<size=35>{biasInfo[2]}</size>";
            }

            return "";
        }
    }
}