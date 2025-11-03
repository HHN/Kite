# Kite2


## About the Project

KITE is an AI-supported, gamified application that enables female founders to explore and respond to discriminatory interaction patterns in entrepreneurial contexts. The project is developed as part of the project KITE II [(BMBFSFJ 2023–2025)](https://www.hs-heilbronn.de/de/kite-II).

This repository contains the Unity project, a collection of visual novels. Each novel deals with its own theme, such as the relationship with parents, dealing with the press, or applying for a loan.

## Project Structure

The project is divided into different modules located in the `Assets` folder:

*   `_novels_twee`: Contains the individual visual novels, which are written in Twee.
*   `_Scenes`: The Unity scenes that display the visual novels.
*   `_Scripts`: C# scripts for the game logic.
*   `_Images`, `_AudioResources`, `_Shader`: Graphical and audio resources.

## Getting Started

To open and edit the project, you need Unity.

1.  Clone the repository.
2.  Open the project in Unity.
3.  The main scene to start the game is `Assets/_Scenes/MainMenuScene.unity`.

### Configure Game View

For optimal display and testing of the mobile application:

1.  Open the Game window (`Window > General > Game`).
2.  In the Game window toolbar, switch from Game" to **Simulator**.
3.  Select a mobile device (e.g., **Samsung Galaxy Note20 Ultra 5G**).

This ensures the game is displayed in the correct mobile resolution during development.

## Contributing

If you want to contribute to the project, please note the following points:

*   The individual visual novels are located in the `Assets/_novels_twee` folder.
*   Each novel has its own subfolder.
*   The list of novels is managed in the file `Assets/_novels_twee/list_of_novels.txt`.

## Licenses

This project uses a dual licensing model:

### Software (Code)
The source code (C# scripts, game logic) is licensed under the **MIT License**.
See [LICENSE_SOFTWARE.txt](LICENSE_SOFTWARE.txt) for details.

**Summary:**
- ✅ Free to use, modify, and distribute
- ✅ Commercial and private use permitted
- ⚠️ Copyright notice must be retained

### Content (Assets)
All content (texts, images, audio, visual novel content) is licensed under the **Creative Commons Attribution-ShareAlike 4.0 International License (CC BY-SA 4.0)**.
See [LICENSE_ASSETS.txt](LICENSE_ASSETS.txt) for details.

**Summary:**
- ✅ Free to use, share, and adapt
- ⚠️ Attribution to original creators required
- ⚠️ Share-alike: derivatives must use the same license (CC BY-SA 4.0)

### Unity Packages
This project uses standard Unity packages, which are subject to Unity's respective license terms.

---

## Deutsche Version

# Kite2

## Über das Projekt

KITE ist eine KI-unterstützte, gamifizierte Anwendung, die weiblichen Gründerinnen ermöglicht, diskriminierende Interaktionsmuster in unternehmerischen Kontexten zu erkunden und darauf zu reagieren. Das Projekt wird im Rahmen des Projekts KITE II [(BMBFSFJ 2023–2025)](https://www.hs-heilbronn.de/de/kite-II) entwickelt.

Dieses Repository enthält das Unity-Projekt, eine Sammlung von Visual Novels. Jede Novel behandelt ein eigenes Thema, wie die Beziehung zu den Eltern, den Umgang mit der Presse oder die Beantragung eines Kredits.

## Projektstruktur

Das Projekt ist in verschiedene Module unterteilt, die sich im Ordner `Assets` befinden:

*   `_novels_twee`: Enthält die einzelnen Visual Novels, die in Twee geschrieben sind.
*   `_Scenes`: Die Unity-Szenen, die die Visual Novels darstellen.
*   `_Scripts`: C#-Skripte für die Spiellogik.
*   `_Images`, `_AudioResources`, `_Shader`: Grafische und Audio-Ressourcen.

## Erste Schritte

Um das Projekt zu öffnen und zu bearbeiten, benötigen Sie Unity.

1.  Klonen Sie das Repository.
2.  Öffnen Sie das Projekt in Unity.
3.  Die Hauptszene zum Starten des Spiels ist `Assets/_Scenes/MainMenuScene.unity`.

### Game View konfigurieren

Für die optimale Darstellung und das Testen der mobilen Anwendung:

1.  Öffnen Sie das Game-Fenster (`Window > General > Game`).
2.  In der Leiste des Game-Fensters von "Game" auf **Simulator** umstellen.
3.  Als Device z. B. **Samsung Galaxy Note20 Ultra 5G** auswählen.

So wird das Spiel während der Entwicklung in der korrekten mobilen Auflösung angezeigt.

## Mitwirken

Wenn Sie zum Projekt beitragen möchten, beachten Sie bitte die folgenden Punkte:

*   Die einzelnen Visual Novels befinden sich im Ordner `Assets/_novels_twee`.
*   Jede Novel hat ihren eigenen Unterordner.
*   Die Liste der Novels wird in der Datei `Assets/_novels_twee/list_of_novels.txt` verwaltet.

## Lizenzen

Dieses Projekt verwendet eine duale Lizenzierung:

### Software (Code)
Der Quellcode (C#-Skripte, Spiellogik) steht unter der **MIT-Lizenz**.
Details siehe [LICENSE_SOFTWARE.txt](LICENSE_SOFTWARE.txt)

**Zusammengefasst:**
- ✅ Freie Nutzung, Veränderung und Verteilung
- ✅ Kommerzieller und privater Einsatz erlaubt
- ⚠️ Urheberrechtsvermerk muss erhalten bleiben

### Inhalte (Assets)
Alle Inhalte (Texte, Bilder, Audio, Visual Novel-Inhalte) stehen unter der **Creative Commons Attribution-ShareAlike 4.0 International License (CC BY-SA 4.0)**.
Details siehe [LICENSE_ASSETS.txt](LICENSE_ASSETS.txt)

**Zusammengefasst:**
- ✅ Freie Nutzung, Teilen und Bearbeitung
- ⚠️ Namensnennung der Urheber*innen erforderlich
- ⚠️ Weitergabe unter gleichen Bedingungen (CC BY-SA 4.0)

### Unity Packages
Das Projekt verwendet Standard-Unity-Packages, die den jeweiligen Unity-Lizenzbedingungen unterliegen.