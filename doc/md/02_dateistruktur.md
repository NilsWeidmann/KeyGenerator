[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Vorbereitungen →](03_vorbereitungen.md)

---

# 2. Dateistruktur

Die Anwendung benötigt zum Start lediglich die ausführbare Datei (Dateiendung `.exe`).
Eine eingebettete Konfigurationsdatei enthält die Spielplan-Zuordnungen (ähnliche, gegenläufige und parallele Schlüsselzahlen je Rastergröße), die Standard-Referenzrastergrößen sowie die unterstützten Altersklassen.
Beim Arbeiten mit dem Tool werden folgende Dateien erzeugt bzw. verwendet:

| Name | Bedeutung |
|------|-----------|
| `KeyGenerator.exe` | Eigentliche Anwendung |
| `Data.json` | Arbeitsdatei mit allen Gruppen-, Vereins- und Mannschaftsdaten |
| `Log.csv` | Protokolldatei der Generierungsläufe (Zeitstempel, Laufzeit, Konflikte, Status) |
| `Terminmeldung.csv` | Exportierte Spieltagsinformationen je Mannschaft |
| `Results.csv` | Exportierte Ergebnisse der Schlüsselzahlgenerierung |

Die Daten werden im JSON-Format gespeichert und geladen.
Ein älteres CSV-Format wird weiterhin unter "Sonstiges" unterstützt (siehe [7. Sonstige Funktionen](07_sonstige_funktionen.md)).

---

[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Vorbereitungen →](03_vorbereitungen.md)
