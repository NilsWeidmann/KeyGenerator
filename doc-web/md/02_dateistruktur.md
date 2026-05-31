[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Datenimport →](03_datenimport.md)

---

# 2. Dateistruktur

Die Web-Anwendung benötigt keine Installation und wird direkt im Browser geöffnet.
Alle Arbeitsdaten werden im Browser-Speicher (LocalStorage) gehalten und bleiben auch nach dem Schließen des Browser-Tabs erhalten, sofern der Browser-Cache nicht geleert wird.
Eine explizite Sicherheitskopie der Arbeitsdaten kann jederzeit über den Button **Speichern** (oder **Strg+S**) in der Übersicht als JSON-Datei heruntergeladen werden.

Beim Arbeiten mit dem Tool werden folgende Dateien erzeugt bzw. verwendet:

| Name | Bedeutung |
|------|-----------|
| `Data.json` | Arbeitsdatei mit allen Gruppen-, Vereins- und Mannschaftsdaten. Kann über den Button **Speichern** heruntergeladen und über die Datenimport-Seite wieder geladen werden. |
| `Terminmeldung.csv` | Exportierte Spieltagsinformationen je Mannschaft. Wird über die Seitenleiste als Download bereitgestellt. |
| `Results.csv` | Exportierte Ergebnisse der Schlüsselzahlgenerierung. Wird über die Seitenleiste als Download bereitgestellt. |

Die Daten werden im JSON-Format gespeichert und geladen.
Ein älteres CSV-Format wird weiterhin unterstützt (siehe [7.6 CSV exportieren und importieren](07_sonstige_funktionen.md#76-csv-exportieren-und-importieren-historisches-format)).

---

[← 1. Einleitung](01_einleitung.md) | [Inhaltsverzeichnis](README.md) | [3. Datenimport →](03_datenimport.md)
