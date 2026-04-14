[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Fensternavigation →](10_navigation.md)

---

# 9. Fehlerbehebung

| Problem | Lösung |
|---------|--------|
| "Es konnten keine Schlüsselzahlen ermittelt werden!" | Widersprüchliche Vorgaben überprüfen. Laufzeit erhöhen. Feste Vorgaben reduzieren. |
| "Inkonsistenter Spielplan"-Meldung | Heim-/Auswärtsspielvorgaben für das genannte Team überprüfen. Sicherstellen, dass Teams desselben Vereins in derselben Spielwoche kompatible Vorgaben haben. |
| Schlüsselzahlen bei Referenzrasteränderung zurückgesetzt | Das ist erwartetes Verhalten. Schlüsselzahlen, die den neuen Bereich überschreiten, werden automatisch auf 0 zurückgesetzt. |
| Button "Generieren" ist nicht aktiviert | Stellen Sie sicher, dass Daten geladen oder importiert wurden. |
| Buttons für Datenimport sind nicht aktiviert | Stellen Sie sicher, dass beide Referenzraster eingestellt sind. |
| Fehlermeldung "Config validation failed" beim Programmstart oder Konfigurationsimport | Die geladene Konfigurationsdatei enthält ungültige Werte (z.B. leere Altersklassen, widersprüchliche Rastergrößen oder fehlerhafte Spielplan-Einträge). Die Fehlermeldung listet die konkreten Verstöße auf. Exportieren Sie über **Sonstiges** → **Konfiguration exportieren** eine gültige Konfiguration und verwenden Sie diese als Vorlage. |

---

[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Fensternavigation →](10_navigation.md)
