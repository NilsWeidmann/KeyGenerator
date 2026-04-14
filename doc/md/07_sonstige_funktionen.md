[← 6. Schlüsselzahlen generieren](06_generierung.md) | [Inhaltsverzeichnis](README.md) | [8. Typischer Arbeitsablauf →](08_arbeitsablauf.md)

---

# 7. Sonstige Funktionen

Über den Button **Sonstiges** auf dem Startbildschirm öffnet sich ein Fenster mit weiteren Funktionen:

| Funktion | Beschreibung |
|----------|--------------|
| Backup laden | Lädt eine zuvor gespeicherte Sicherheitskopie (z.B. den Stand vor der letzten Generierung). Siehe Abschnitt 7.1. |
| Ergebnisse exportieren | Exportiert die generierten Schlüsselzahlen als CSV-Datei mit Gruppen, Mannschaften, Schlüsselzahlen, Wunsch-Schlüsselzahlen, Spielwochen und Zusatz-Vorgaben. Falls die zugewiesene Schlüsselzahl nicht in der Wunschliste enthalten ist (Konflikt), wird dies in der Spalte "Wunsch" sichtbar. |
| Terminmeldung speichern | Exportiert eine CSV-Datei mit Spieltag (Wochentag + Uhrzeit) und Ersatzspieltag je Mannschaft. Nützlich, um die Vollständigkeit der Terminmeldung zu überprüfen. |
| Konflikte neu auflösen | Setzt alle Mannschafts-Schlüsselzahlen auf die aus dem Vereinswochenschema abgeleiteten Werte zurück und startet die Konflikterkennung/-auflösung erneut. Nützlich, wenn die Konfliktauflösung unterbrochen wurde, oder die Konflikte neu bewertet werden sollen. |
| Generator-Tests | Startet automatisierte Tests des Generierungsalgorithmus (für die normale Anwendung nicht notwendig). |
| Tests aus Datei | Führt Tests aus einer Datei aus (für die normale Anwendung nicht notwendig). |
| Konfiguration exportieren | Exportiert die aktuelle Konfiguration an einen gewählten Speicherort. Die Konfiguration enthält Heim- und Auswärtsspieltage je Raster, die Referenzrastergrößen, die minimale/maximale Rastergröße sowie die unterstützten Altersklassen. |
| Konfiguration importieren | Importiert eine Konfiguration aus einer JSON-Datei und ersetzt damit alle oben genannten Einstellungen (Spielpläne, Referenzraster, Rastergrößen, Altersklassen). |
| CSV exportieren (histor.) | Exportiert die Daten in ein älteres CSV-Format. |
| CSV importieren (histor.) | Importiert Daten aus einem älteren CSV-Format. |
| Abbrechen | Schließt das Fenster und kehrt zum Startbildschirm zurück. |

## 7.1 Backup laden

Vor jeder Generierung wird automatisch eine Sicherheitskopie des aktuellen Datenstands erstellt.
Falls Sie nach der Generierung einen Fehler bemerken, können Sie den vorherigen Zustand wiederherstellen:

1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **Backup laden**.
3. Es öffnet sich ein Dateidialog, der automatisch den Unterordner `Backup` im eingestellten Verzeichnis vorauswählt. Wählen Sie dort die gewünschte Sicherheitskopie (JSON-Datei) aus.
4. Der geladene Zustand wird sofort wieder abgespeichert.

## 7.2 Ergebnisse exportieren

Nach der Generierung können Sie die Ergebnisse als CSV-Datei speichern:

1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **Ergebnisse exportieren**.
3. Wählen Sie im Dateidialog den gewünschten Speicherort und Dateinamen.

Die exportierte CSV-Datei enthält je Mannschaft: Gruppe, Mannschaftsname, zugewiesene Schlüsselzahl, Wunsch-Schlüsselzahlen, Spielwochen sowie Zusatzvorgaben zu Heim- und Auswärtsspieltagen.
Falls die zugewiesene Schlüsselzahl nicht in der Wunschliste enthalten ist (Konflikt), wird dies in der Spalte "Wunsch" kenntlich gemacht.

## 7.3 Terminmeldung speichern

Mit dieser Funktion können Sie die eingegebenen Terminmeldungen zur Überprüfung exportieren:

1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **Terminmeldung speichern**.
3. Wählen Sie im Dateidialog den gewünschten Speicherort und Dateinamen.

Die erzeugte CSV-Datei enthält je Mannschaft den gemeldeten Spieltag (Wochentag und Uhrzeit) sowie den Ersatzspieltag.
Sie eignet sich dazu, die Vollständigkeit der Terminmeldung zu überprüfen.

## 7.4 Konflikte neu auflösen

Falls die Konfliktauflösung unterbrochen wurde oder die Ergebnisse neu bewertet werden sollen, kann sie erneut gestartet werden:

1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **Konflikte neu auflösen**.

Dabei werden alle Mannschafts-Schlüsselzahlen auf die aus dem Vereinswochenschema abgeleiteten Ausgangswerte zurückgesetzt und die Konflikterkennung sowie -auflösung werden erneut durchgeführt.
Bereits vorgenommene Anpassungen an den Schlüsselzahlen gehen dabei verloren.

## 7.5 Konfiguration exportieren und importieren

Die Konfiguration legt fest, welche Spielpläne (Heim-/Auswärtsspieltage je Raster), Referenzrastergrößen, minimale und maximale Rastergrößen sowie Altersklassen unterstützt werden.
Sie kann gespeichert und auf einem anderen Rechner oder in einer anderen Saison wiederverwendet werden.

**Konfiguration exportieren:**
1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **Konfiguration exportieren**.
3. Wählen Sie im Dateidialog den gewünschten Speicherort und Dateinamen. Die Konfiguration wird als JSON-Datei gespeichert.

**Konfiguration importieren:**
1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **Konfiguration importieren**.
3. Wählen Sie im Dateidialog die gewünschte JSON-Konfigurationsdatei aus.

Beim Import werden alle bestehenden Einstellungen (Spielpläne, Referenzraster, Rastergrößen, Altersklassen) vollständig durch die Werte aus der importierten Datei ersetzt.
Falls die Datei ungültige Werte enthält, erscheint eine Fehlermeldung mit einer Auflistung der konkreten Verstöße (siehe auch [09_fehlerbehebung.md](09_fehlerbehebung.md)).

## 7.6 CSV exportieren und importieren (historisches Format)

Für den Austausch mit älteren Versionen des Programms steht ein CSV-basiertes Datenformat zur Verfügung.

**CSV exportieren:**
1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **CSV exportieren (historisch)**.
3. Wählen Sie im Verzeichnisdialog den gewünschten Speicherort aus.

**CSV importieren:**
1. Klicken Sie auf **Sonstiges** im Startbildschirm.
2. Klicken Sie auf **CSV importieren (historisch)**.
3. Wählen Sie im Verzeichnisdialog den Speicherort der jeweiligen CSV-Dateien aus.

Beachten Sie, dass dieses Format nur für die Kompatibilität mit älteren Programmversionen vorgesehen ist.
Für die normale Nutzung empfiehlt sich das JSON-basierte Speicherformat (siehe [04_datenimport.md – Abschnitt 4.3](04_datenimport.md)).

---

[← 6. Schlüsselzahlen generieren](06_generierung.md) | [Inhaltsverzeichnis](README.md) | [8. Typischer Arbeitsablauf →](08_arbeitsablauf.md)
