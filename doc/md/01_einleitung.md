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
Nach der Ermittlung einer optimalen Lösung (bzw. der besten innerhalb der maximalen Laufzeit gefundenen Lösung) wird der/dem Spielplaner(in) eine Liste von Interessenskonflikten präsentiert, die im letzten Schritt manuell aufgelöst werden können, um einen fairen Ausgleich zwischen den betroffenen Vereinen herzustellen.

## 1.1 Durchgängiges Beispiel

Um die Konzepte dieser Dokumentation anschaulich zu machen, verwenden wir ein durchgängiges fiktives Beispiel mit Vereinsnamen der 1., 2. und 3. Bundesliga sowie der Regionalliga (Damen + Erwachsene) aus der Saison 2025/26. Die Ligastruktur ist dagegen der von Bezirksligen nachempfunden – so ist klar erkennbar, dass es sich um ein fiktives Beispiel handelt.
Das Beispiel beinhaltet 49 Gruppen mit je 5–12 Mannschaften, insgesamt 492 Mannschaften aus 106 Vereinen.

In der Dokumentation sind Abschnitte, die sich auf dieses Beispiel beziehen, mit dem Vermerk *"Beispiel"* gekennzeichnet.
Das Beispiel kann <a href="../../data/Example.json" download>hier</a> heruntergeladen und in den KeyGenerator importiert werden (siehe [4.3 Laden aus Datei](04_datenimport.md#43-laden-aus-datei)).

---

[← Inhaltsverzeichnis](README.md) | [2. Dateistruktur →](02_dateistruktur.md)
