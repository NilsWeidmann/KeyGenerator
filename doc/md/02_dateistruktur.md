[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Vorbereitungen →](03_vorbereitungen.md)

---

# 2. Dateistruktur

Die Anwendung wird über ein Installationsprogramm (MSI-Datei) installiert und anschließend über `KeyGenerator.exe` gestartet.
Eine eingebettete Standardkonfiguration enthält die Spielpläne (Heim- und Auswärtsspieltage je Schlüsselzahl und Rastergröße), die Standard-Referenzrastergrößen sowie die unterstützten Altersklassen. Welche Schlüsselzahlen ähnlich, gegenläufig oder parallel sind, wird aus den Spielplänen abgeleitet.

Folgende Dateien können in die Anwendung importiert bzw. aus ihr exportiert werden oder werden von ihr angelegt:

| Name | Import/Export | Bedeutung |
|------|---------------|-----------|
| `Tabellen.csv` | Import | Gruppeneinteilung aus Click-TT (Gruppen und ihre Mannschaften). Wird im Fenster „click-tt Import" im Bereich „Gruppen" geladen (siehe [4.1 Import aus Click-TT](04_datenimport.md#41-import-aus-click-tt) und [B.1](B_download_aus_click-tt.md#b1-gruppenstruktur-als-csv-datei-herunterladen)). |
| `Terminmeldung.html` | Import | Aus den Terminmeldungs-PDFs von Click-TT erzeugte HTML-Datei mit den Vereinsinformationen und Spielwochenwünschen. Wird im Fenster „click-tt Import" im Bereich „Terminmeldung" geladen (siehe [4.1 Import aus Click-TT](04_datenimport.md#41-import-aus-click-tt) und [B.2](B_download_aus_click-tt.md#b2-terminmeldung-als-pdf-datei-herunterladen) – [B.4](B_download_aus_click-tt.md#b4-terminmeldung-in-eine-html-datei-konvertieren)). |
| `Data.json` | Import und Export | Arbeitsdatei mit allen Gruppen-, Vereins- und Mannschaftsdaten einschließlich der Zusatzvorgaben, der Partnerschaften und der Referenzraster. Wird über **Speichern** auf dem Startbildschirm (siehe [3. Vorbereitungen](03_vorbereitungen.md)), im Fenster „click-tt Import" oder im Fenster „Dateninput" gespeichert und über **Aus Datei** geladen (siehe [4.3 Laden aus Datei](04_datenimport.md#43-laden-aus-datei)). Die Datei ist mit der Web-Anwendung austauschbar. |
| `Backup\Backup_<Zeitstempel>.json` | Export (automatisch) und Import | Sicherheitskopie, die vor jeder Generierung automatisch im Unterordner `Backup` des eingestellten Verzeichnisses angelegt wird. Laden über **Sonstiges** → **Backup laden** (siehe [7.1 Backup laden](07_sonstige_funktionen.md#71-backup-laden)). |
| `Log.csv` | Export (automatisch) | Protokolldatei der Generierungsläufe (Zeitstempel, Laufzeit, Konflikte, Status) im eingestellten Verzeichnis. |
| `Results.csv` | Export | Ergebnisse der Schlüsselzahlgenerierung. Über **Sonstiges** → **Ergebnisse exportieren** (siehe [7.2 Ergebnisse exportieren](07_sonstige_funktionen.md#72-ergebnisse-exportieren)). |
| `Terminmeldung.csv` | Export | Gemeldeter Spieltag und Ersatzspieltag je Mannschaft. Über **Sonstiges** → **Terminmeldung speichern** (siehe [7.3 Terminmeldung speichern](07_sonstige_funktionen.md#73-terminmeldung-speichern)). |
| `Config.json` | Import und Export | Konfiguration (Spielpläne, Voreinstellungen der Referenzraster, Rastergrößen, Altersklassen usw.). Über **Sonstiges** → **Konfiguration exportieren** bzw. **Konfiguration importieren** (siehe [7.5 Konfiguration exportieren und importieren](07_sonstige_funktionen.md#75-konfiguration-exportieren-und-importieren)). Eine importierte Konfiguration wird beim nächsten Programmstart wieder verwendet. |
| `Staffeln.csv`, `Vereine.csv`, `Beziehungen.csv` | Import und Export | Daten im älteren CSV-Format. Über **Sonstiges** → **CSV exportieren (historisch)** bzw. **CSV importieren (historisch)** (siehe [7.6 CSV exportieren und importieren](07_sonstige_funktionen.md#76-csv-exportieren-und-importieren-historisches-format)). |
| Testdateien (`*.txt`) | Import | Nur für Entwickler: Testinstanzen, die über **Sonstiges** → **Tests aus Datei** aus dem eingestellten Verzeichnis gelesen werden (siehe [7. Sonstige Funktionen](07_sonstige_funktionen.md)). |

Die Daten werden im JSON-Format gespeichert und geladen.
Ein älteres CSV-Format wird weiterhin unterstützt (siehe [7.6 CSV exportieren und importieren](07_sonstige_funktionen.md#76-csv-exportieren-und-importieren-historisches-format)).

---

[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Vorbereitungen →](03_vorbereitungen.md)
