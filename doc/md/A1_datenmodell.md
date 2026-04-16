[← 10. Fensternavigation](10_navigation.md) | [Inhaltsverzeichnis](README.md)

---

# Anhang A: Datenmodell

Zum besseren Verständnis der Anwendung wird hier das zugrundeliegende Datenmodell kurz erläutert.

## A.1 Verein

Zu einem Verein werden folgende Daten gespeichert:
- **Name**: Vereinsname (Umlaute werden automatisch ersetzt).
- **Schlüsselzahlen (A, B, X, Y)**: Zugeordnete Schlüsselzahlen auf Vereinsebene.
- **Mannschaften**: Liste aller Mannschaften des Vereins über alle Gruppen hinweg.

## A.2 Gruppe

Zu einer Gruppe werden folgende Daten gespeichert:
- **Name**: Bezeichnung der Gruppe (z.B. "Bezirksoberliga Erwachsene").
- **Rastergröße**: Die Rastergröße (6, 8, 10, 12 oder 14), die bestimmt, wie viele Schlüsselzahlen in dieser Gruppe vergeben werden.
- **Mannschaften**: Die in dieser Gruppe spielenden Teams (maximal so viele wie die Rastergröße).

## A.3 Mannschaft

Eine Mannschaft gehört zu genau einem Verein und genau einer Gruppe:
- **Name**: Vollständiger Name (Vereinsname + Mannschaftsnummer).
- **Spielwoche**: A, B, X, Y oder `-` (nicht zugeordnet).
- **Schlüsselzahl**: Die zugewiesene Schlüsselzahl (0 = nicht zugewiesen).
- **Spieltage**: Liste mit Vorgaben für Heim- und Auswärtsspiele je Spieltag.
- **Altersklasse**: Zugehörige Altersklasse (z.B. Erwachsene, Jugend 19, Senioren 40 usw.).

## A.4 Schlüsselzahlen und Spielplan

Die Schlüsselzahl eines Teams bestimmt dessen Spielplan, d.h. an welchen Spieltagen Heim- und Auswärtsspiele stattfinden.
Die Zuordnung zwischen Schlüsselzahlen und Spielplänen sowie die gültigen Rastergrößen und Referenzraster sind in einer voreingestellten Konfiguration hinterlegt, können aber angepasst werden (siehe [7.5 Konfiguration exportieren und importieren](07_sonstige_funktionen.md#75-konfiguration-exportieren-und-importieren)).

Zentrale Konzepte:
- **Gegenläufige Schlüsselzahlen**: Zwei Schlüsselzahlen Z1 und Z2 sind gegenläufig, wenn für jede Spielwoche gilt: Z1 hat genau dann ein Heimspiel, wenn Z2 ein Auswärtsspiel hat, und umgekehrt. A und B bzw. X und Y eines Vereins erhalten immer gegenläufige Schlüsselzahlen.
- **Parallele Schlüsselzahlen**: Bei unterschiedlichen Rastergrößen (z.B. Referenzraster vs. Gruppenraster) werden zwei Schlüsselzahlen Z1 und Z2 als parallel zueinander bezeichnet, wenn in keiner Spielwoche Z1 ein Heimspiel und Z2 ein Auswärtsspiel hat, oder umgekehrt.
- **Ähnliche Schlüsselzahlen**: Schlüsselzahlen, deren Spielpläne sich nur an maximal zwei Spieltagen bzgl. ihres Musters für Heim- und Auswärtsspiele unterscheiden. Diese werden bei der Konfliktauflösung als Alternative angeboten.

> **Beispiel: Gegenläufige und parallele Schlüsselzahlen**
>
> Am Beispiel Borussia Düsseldorf lassen sich diese Konzepte gut veranschaulichen:
>
> **Gegenläufige Schlüsselzahlen:** Der Verein hat Schlüsselzahl A=5 und B=11 (Referenzraster 12). Die Zahl 11 ist die gegenläufige Zahl zu 5 im 12er-Raster: Wenn Schlüsselzahl 5 an einem bestimmten Spieltag ein Heimspiel vorsieht, hat Schlüsselzahl 11 an diesem Spieltag ein Auswärtsspiel und umgekehrt. Ebenso gilt: X=5 und Y=10 sind gegenläufig im 10er-Raster.
>
> **Parallele Schlüsselzahlen:** Borussia Düsseldorf I spielt in der Bezirksoberliga (Rastergröße 12) mit Vereins-Schlüsselzahl A=5 und erhält die Mannschafts-Schlüsselzahl 5. Die Damen-Mannschaft spielt dagegen in einer Gruppe mit Rastergröße 10. Auch sie ist in Woche A und erhält ebenfalls Schlüsselzahl 5 – in diesem Fall ist die parallele Schlüsselzahl im 10er-Raster zufällig identisch. Wäre die Damen-Mannschaft der Woche B zugeordnet, würde sie die Schlüsselzahl 10 bekommen, da die Zahl 10 im 10er-Raster parallel zu den Zahlen 11 und 12 im 12er-Raster ist.
>
> **Ähnliche Schlüsselzahlen:** In der Jugend 13 2. Bezirksliga 3 spielen zwei Teams des Vereins in derselben Gruppe (Rastergröße 10), und sind beide der Spielwoche X zugeordnet. Das erste Team erhält die Schlüsselzahl 5, das zweite Team kann aber nicht ebenfalls Schlüsselzahl 5 erhalten und bekommt stattdessen die *ähnliche* Schlüsselzahl 4, die einen ähnlichen, aber nicht identischen Spielplan ergibt.

## A.5 Spielwochen

Die Spielwochen bestimmen, in welchem Rhythmus eine Mannschaft ihre Heimspiele austrägt.
Auf Vereinsebene wird pro Spielwoche eine Schlüsselzahl ermittelt, wobei für die jeweiligen Spielwochenpaare gegenläufige Schlüsselzahlen vergeben werden.
Für jede Mannschaft, die einer Spielwoche S zugeordnet ist, ist garantiert, dass sie eine Schlüsselzahl erhält, die zu derjenigen Schlüsselzahl ähnlich ist, die für ihren Verein für Spielwoche S vergeben wurde, oder mit dieser exakt übereinstimmt.

> **Beispiel: Spielwochen am Beispiel Borussia Düsseldorf**
>
> Borussia Düsseldorf nutzt alle vier Spielwochen:
>
> | Woche | Altersklassen | Ref.-Raster | Vereins-SZ | Anz. Teams |
> |-------|---------------|-------------|------------|------------|
> | A | Erwachsene I & II, Damen | 12 | 5 | 3 |
> | B | Erwachsene III–V | 12 | 11 | 3 |
> | X | Jugend 19 I & III, 15 I, 13 I & II | 10 | 5 | 5 |
> | Y | Jugend 19 II & IV, 15 II | 10 | 10 | 3 |
>
> Die Erwachsenen-Mannschaften I und II und die Damen-Mannschaft spielen in Woche A, die Erwachsenen III–V in der gegenläufigen Woche B. So haben beide Gruppen abwechselnd Heimrecht in der Halle. Die Jugendmannschaften nutzen die Wochen X und Y, die einem anderen Spielrhythmus (Referenzraster 10) folgen.

---

[← 10. Fensternavigation](10_navigation.md) | [Inhaltsverzeichnis](README.md)
