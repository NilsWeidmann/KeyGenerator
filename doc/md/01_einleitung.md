[← Inhaltsverzeichnis](README.md) | [2. Dateistruktur →](02_dateistruktur.md)

---

# 1. Einleitung

Das Schlüsselzahlen-Generierungstool dient der automatischen Zuweisung von Schlüsselzahlen für Mannschaften in Sportarten mit Liga-Spielbetrieb (z.B. Tischtennis).
Schlüsselzahlen bestimmen den Spielplan einer Mannschaft, d.h. an welchen Spieltagen Heim- bzw. Auswärtsspiele stattfinden.
Die Anwendung löst ein komplexes Zuordnungsproblem unter Berücksichtigung zahlreicher Nebenbedingungen, etwa:

- Vereine mit mehreren Mannschaften benötigen zueinander kompatible Schlüsselzahlen (gegenläufig, parallel, ähnlich).
- Vorgegebene Schlüsselzahlen von höheren Gliederungsebenen (Bezirk/Verband) müssen respektiert werden.
- Einzelne Spieltage können als Heim- oder Auswärtsspiel vorgegeben werden.

Diese Nebenbedingungen werden von den Vereinen einer Gliederung im Vorfeld formuliert, und durch das Tool in ein Optimierungsproblem übersetzt.
Nach der Ermittlung einer optimalen Lösung wird der/dem Spielplaner(in) eine Liste von Interessenskonflikten präsentiert, die im letzten Schritt manuell aufgelöst werden können, um einen fairen Ausgleich zwischen den betroffenen Vereinen herzustellen.

## 1.1 Durchgängiges Beispiel

Um die Konzepte dieser Dokumentation anschaulich zu machen, verwenden wir ein durchgängiges fiktives Beispiel mit den Bundesligisten der Saison 2025/26.
In diesem Beispiel hat Borussia Düsseldorf 14 Mannschaften in 13 verschiedenen Gruppen und 5 Altersklassen (Erwachsene, Damen, Jugend 19, Jugend 15, Jugend 13), und deckt damit ein breites Spektrum an Anwendungsfällen ab.
Die oberste Liga ist die Bezirksoberliga Erwachsene, eine vollbesetzte Gruppe mit Rastergröße 12, in der alle 12 Schlüsselzahlen genau einmal vergeben wurden.

In der Dokumentation sind Abschnitte, die sich auf dieses Beispiel beziehen, mit dem Vermerk *"Beispiel"* gekennzeichnet.

---

[← Inhaltsverzeichnis](README.md) | [2. Dateistruktur →](02_dateistruktur.md)
