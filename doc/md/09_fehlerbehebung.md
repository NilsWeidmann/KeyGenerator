[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Fensternavigation →](10_navigation.md)

---

# 9. Fehlerbehebung

Die folgende Tabelle enthält eine Übersicht über häufig auftretende Fehler und ihre Behebung:

| Problem | Lösung |
|---------|--------|
| "Es konnten keine Schlüsselzahlen ermittelt werden!" | Widersprüchliche Vorgaben überprüfen. Laufzeit erhöhen. Feste Vorgaben reduzieren. |
| "Inkonsistenter Spielplan"-Meldung | Heim-/Auswärtsspielvorgaben für das genannte Team überprüfen. Sicherstellen, dass Teams desselben Vereins in derselben Spielwoche kompatible Vorgaben haben. |
| Schlüsselzahlen bei Referenzrasteränderung zurückgesetzt | Das ist erwartetes Verhalten. Schlüsselzahlen, die den neuen Bereich überschreiten, werden automatisch auf 0 zurückgesetzt; eine Meldung nennt die betroffenen Vereine. Tragen Sie dort bei Bedarf neue Schlüsselzahlen ein. |
| "Ungültige Rastergröße" | Die eingegebene Rastergröße ist für die Gruppe nicht zulässig und wurde zurückgesetzt. Die Meldung nennt die zulässigen Rastergrößen (siehe [5.2 Gruppensicht](05_startbildschirm.md#52-gruppensicht)). |
| Button "Generieren" ist nicht aktiviert | Stellen Sie sicher, dass Daten geladen oder importiert wurden. |
| Buttons für Datenimport sind nicht aktiviert | Stellen Sie sicher, dass beide Referenzraster eingestellt sind. |
| Fehlermeldung "Das Referenzraster … wird von der aktuellen Konfiguration nicht unterstützt" beim Laden einer Datei | Die Datei verwendet ein Referenzraster, für das die aktuelle Konfiguration keinen Spielplan enthält. Importieren Sie die Konfiguration, mit der die Datei erstellt wurde (siehe [7.5 Konfiguration exportieren und importieren](07_sonstige_funktionen.md#75-konfiguration-exportieren-und-importieren)), und laden Sie die Datei erneut. |
| Fehlermeldung "Die Konfiguration ist ungültig" beim Programmstart oder Konfigurationsimport | Die geladene Konfigurationsdatei enthält ungültige Werte (z.B. leere Altersklassen, widersprüchliche Rastergrößen oder fehlerhafte Spielplan-Einträge). Die Fehlermeldung listet die konkreten Verstöße auf. Exportieren Sie über **Sonstiges** → **Konfiguration exportieren** eine gültige Konfiguration und verwenden Sie diese als Vorlage. |

---

[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Fensternavigation →](10_navigation.md)
