[← 9. Fehlerbehebung](09_fehlerbehebung.md) | [Inhaltsverzeichnis](README.md) | [A. Datenmodell →](A_datenmodell.md)

---

# 10. Seitennavigation

Die folgende Übersicht zeigt alle Seiten der Web-Anwendung und wie diese von der Seitenleiste bzw. von anderen Seiten aus erreichbar sind:

- **Seitenleiste → Datenimport** → Datenimport-Seite (`/`)
  - Grundeinstellungen: Referenzraster A/B und X/Y, **Maximale Laufzeit**, **Interne Duelle möglichst vorziehen**
  - Schritt 1: Gruppeneinteilung laden (CSV-Datei)
  - Schritt 2: Terminmeldung laden (HTML-Datei) – verfügbar nach Schritt 1
  - Schritt 3: Zwischenstand aus Datei laden (JSON-Datei) – unabhängig von Schritt 1
  - Schaltfläche **Zur Übersicht →** – verfügbar nach Schritt 1
- **Seitenleiste → Manuelle Eingabe** → Seite "Manuelle Eingabe" (`/data-entry`) – immer verfügbar; sind noch keine Daten geladen, beginnt die Seite mit einem leeren Datenbestand
  - Linke Spalte oben: Gruppentabelle
  - Linke Spalte unten: Vereinstabelle
  - Rechte Spalte: Mannschaftstabelle (Gruppen- oder Vereinsansicht)
  - Bearbeiten-Symbol bei Team → Dialog "Zusatz"
  - Schaltfläche **Speichern & zur Übersicht** → Übersicht
- **Seitenleiste → Übersicht** → Seite "Übersicht" (`/editor`) – erst nach dem Laden von Daten
  - Umschaltleiste **Gruppenansicht** / **Vereinsansicht** → Ansichtswechsel
  - Bearbeiten-Symbol bei Team → Dialog "Zusatz"
  - Schaltfläche **Partner** (Vereinsansicht) → Dialog "Partnerschaften" des ausgewählten Vereins
  - Schaltfläche **Generieren** → Backup anlegen → Seite "Optimierung"
  - Dropdown **Backup laden** → Auswahl einer Sicherheitskopie (deaktiviert "Konflikte auflösen")
  - Schaltfläche **Speichern** → Download `Data.json`
- **Optimierung** (`/solving/…`)
  - Fortschrittsbalken mit Status
  - Schaltfläche **Abbrechen** – während der Ausführung
  - Schaltfläche **Konflikte auflösen →** – nach Abschluss, falls Konflikte vorhanden
  - Schaltfläche **Zurück zur Übersicht** – nach Abschluss
- **Seitenleiste → Konflikte auflösen** → Seite "Konflikte auflösen" (`/conflicts`) – nur nach abgeschlossener Generierung, bis ein Backup geladen oder neue Daten importiert werden
  - Linke Spalte: Liste der Konflikte
  - Rechte Spalte: Betroffene Teams mit Schlüsselzahl-Dropdowns
  - Schaltfläche **Vorschlag** → automatischer Lösungsvorschlag
  - Schaltfläche **Anwenden & zur Übersicht** → Übersicht

Nach dem Laden von Daten bietet die Seitenleiste außerdem folgende Funktionen:

- **Seitenleiste → Ergebnisse exportieren** → Download `Results.csv`
- **Seitenleiste → Terminmeldung exportieren** → Download `Terminmeldung.csv`
- **Seitenleiste → Konfiguration exportieren** → Download Konfiguration (JSON)
- **Seitenleiste → Konfiguration importieren** → Dateiauswahl-Dialog
- **Seitenleiste → CSV exportieren (historisch)** → Download CSV
- **Seitenleiste → CSV importieren (historisch)** → Dateiauswahl-Dialog

---

[← 9. Fehlerbehebung](09_fehlerbehebung.md) | [Inhaltsverzeichnis](README.md) | [A. Datenmodell →](A_datenmodell.md)
