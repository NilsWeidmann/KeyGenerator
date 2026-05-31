[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Seitennavigation →](10_navigation.md)

---

# 9. Fehlerbehebung

Die folgende Tabelle enthält eine Übersicht über häufig auftretende Fehler und ihre Behebung:

| Problem | Lösung |
|---------|--------|
| "Es konnten keine Schlüsselzahlen ermittelt werden!" | Widersprüchliche Vorgaben überprüfen. Erneute Generierung versuchen. Feste Vorgaben reduzieren. |
| "Inkonsistenter Spielplan"-Meldung | Heim-/Auswärtsspielvorgaben für das genannte Team überprüfen. Sicherstellen, dass Teams desselben Vereins in derselben Spielwoche kompatible Vorgaben haben. |
| Schlüsselzahlen bei Referenzrasteränderung zurückgesetzt | Das ist erwartetes Verhalten. Schlüsselzahlen, die den neuen Bereich überschreiten, werden automatisch auf 0 zurückgesetzt. |
| Schaltfläche "Generieren" ist nicht aktiviert | Stellen Sie sicher, dass Daten geladen oder importiert wurden und die Übersicht geöffnet ist. |
| Schaltflächen für Datenimport sind nicht aktiviert | Stellen Sie sicher, dass zunächst eine CSV-Datei mit der Gruppenstruktur geladen wurde (Schritt 1 auf der Datenimport-Seite). Alternativ kann auch ein gespeicherter Zwischenstand über Schritt 3 ("Zwischenstand aus Datei laden") geladen werden, ohne dass Schritt 1 abgeschlossen sein muss. |
| Fehlermeldung "Config validation failed" beim Konfigurationsimport | Die geladene Konfigurationsdatei enthält ungültige Werte (z.B. leere Altersklassen, widersprüchliche Rastergrößen oder fehlerhafte Spielplan-Einträge). Die Fehlermeldung listet die konkreten Verstöße auf. Exportieren Sie über die Seitenleiste → **Konfiguration exportieren** eine gültige Konfiguration und verwenden Sie diese als Vorlage. |
| Daten nach Seiten-Neuladen verloren | Die Browser-Sitzung wurde zurückgesetzt. Laden Sie künftig regelmäßig einen Zwischenstand über **Speichern** als `Data.json` herunter, damit die Daten dauerhaft gesichert sind. |

---

[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Seitennavigation →](10_navigation.md)
