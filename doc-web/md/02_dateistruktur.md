[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Datenimport →](03_datenimport.md)

---

# 2. Dateistruktur

Die Web-Anwendung benötigt keine Installation und wird direkt im Browser geöffnet.
Alle Arbeitsdaten werden im Browser-Speicher (LocalStorage) gehalten und bleiben auch nach dem Schließen des Browser-Tabs erhalten, sofern der Browser-Speicher nicht gelöscht wird.
Auch die automatisch angelegten Backups und die aktuelle Konfliktliste werden im Browser-Speicher abgelegt; reicht der Speicherplatz nicht aus, werden zuerst die ältesten Backups verworfen (siehe [7.1 Backup laden](07_sonstige_funktionen.md#71-backup-laden)).
Eine explizite Sicherheitskopie der Arbeitsdaten kann jederzeit über den Button **Speichern** (oder **Strg+S**) in der Übersicht als JSON-Datei heruntergeladen werden.

Folgende Dateien können in die Anwendung importiert bzw. aus ihr exportiert werden. Alle Exporte werden als Download des Browsers bereitgestellt.

| Name | Import/Export | Bedeutung |
|------|---------------|-----------|
| `Tabellen.csv` | Import | Gruppeneinteilung aus Click-TT (Gruppen und ihre Mannschaften). Wird auf der Datenimport-Seite in Schritt 1 geladen (siehe [3.2 Import aus Click-TT](03_datenimport.md#32-import-aus-click-tt) und [B.1](B_download_aus_click-tt.md#b1-gruppenstruktur-als-csv-datei-herunterladen)). |
| Terminmeldung (HTML-Datei) | Import | Aus den Terminmeldungs-PDFs von Click-TT erzeugte HTML-Datei mit den Vereinsinformationen und Spielwochenwünschen. Wird auf der Datenimport-Seite in Schritt 2 geladen (siehe [3.2 Import aus Click-TT](03_datenimport.md#32-import-aus-click-tt) und [B.2](B_download_aus_click-tt.md#b2-terminmeldung-als-pdf-datei-herunterladen) – [B.4](B_download_aus_click-tt.md#b4-terminmeldung-in-eine-html-datei-konvertieren)). |
| `Data.json` | Import und Export | Arbeitsdatei mit allen Gruppen-, Vereins- und Mannschaftsdaten einschließlich der Zusatzvorgaben, der Partnerschaften und der Referenzraster. Wird über den Button **Speichern** in der Übersicht heruntergeladen und auf der Datenimport-Seite in Schritt 3 wieder geladen (siehe [3.3 Laden aus Datei](03_datenimport.md#33-laden-aus-datei)). Die Datei ist mit der Desktop-Anwendung austauschbar. |
| `Results.csv` | Export | Ergebnisse der Schlüsselzahlgenerierung. Download über **Ergebnisse exportieren** in der Seitenleiste (siehe [7.2 Ergebnisse exportieren](07_sonstige_funktionen.md#72-ergebnisse-exportieren)). |
| `Terminmeldung.csv` | Export | Gemeldeter Spieltag und Ersatzspieltag je Mannschaft. Download über **Terminmeldung exportieren** in der Seitenleiste (siehe [7.3 Terminmeldung exportieren](07_sonstige_funktionen.md#73-terminmeldung-exportieren)). |
| `Config.json` | Import und Export | Konfiguration (Spielpläne, Voreinstellungen der Referenzraster, Rastergrößen, Altersklassen usw.). Über **Konfiguration exportieren** bzw. **Konfiguration importieren** in der Seitenleiste (siehe [7.4 Konfiguration exportieren und importieren](07_sonstige_funktionen.md#74-konfiguration-exportieren-und-importieren)). |
| `Historical.zip` | Import und Export | ZIP-Datei mit den Dateien `Staffeln.csv`, `Vereine.csv` und `Beziehungen.csv` im älteren CSV-Format. Über **CSV exportieren (historisch)** bzw. **CSV importieren (historisch)** in der Seitenleiste (siehe [7.5 CSV exportieren und importieren](07_sonstige_funktionen.md#75-csv-exportieren-und-importieren-historisches-format)). |
| `Log.csv` | Export | Protokoll der Generierungsläufe der aktuellen Sitzung (Zeitstempel, Laufzeit, Anzahl der Konflikte, Status). Download über **Log exportieren** in der Seitenleiste, sobald mindestens eine Generierung durchgeführt wurde. |

Die Daten werden im JSON-Format gespeichert und geladen.
Ein älteres CSV-Format wird weiterhin unterstützt (siehe [7.5 CSV exportieren und importieren](07_sonstige_funktionen.md#75-csv-exportieren-und-importieren-historisches-format)).

---

[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Datenimport →](03_datenimport.md)
