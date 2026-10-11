[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Seitennavigation →](10_navigation.md)

---

# 9. Fehlerbehebung

Die folgende Tabelle enthält eine Übersicht über häufig auftretende Fehler und ihre Behebung:

| Problem | Lösung |
|---------|--------|
| "Es konnten keine Schlüsselzahlen ermittelt werden!" | Widersprüchliche Vorgaben überprüfen. Erneute Generierung versuchen. Feste Vorgaben reduzieren. |
| "Inkonsistenter Spielplan"-Meldung | Heim-/Auswärtsspielvorgaben für das genannte Team überprüfen. Sicherstellen, dass Teams desselben Vereins in derselben Spielwoche kompatible Vorgaben haben. |
| Schlüsselzahlen bei Referenzrasteränderung zurückgesetzt | Das ist erwartetes Verhalten. Die Schlüsselzahlen der Spielwochen, deren Referenzraster geändert wurde, passen nicht mehr zum Raster und werden bei allen Vereinen auf 0 zurückgesetzt; ein Hinweis nennt die betroffenen Vereine. Tragen Sie dort bei Bedarf neue Schlüsselzahlen ein. |
| Fehlermeldung "Die Gruppe enthält noch Mannschaften. Bitte löschen Sie zunächst diese!" beim Löschen einer Gruppe | Eine Gruppe kann nur gelöscht werden, wenn sie keine Mannschaften mehr enthält; es wird nichts gelöscht. Entfernen Sie zunächst alle Mannschaften aus der Gruppe und löschen Sie danach die Gruppe (siehe [4. Manuelle Eingabe: Gruppentabelle](04_manuelle_eingabe.md#linke-spalte-gruppentabelle)). |
| Fehlermeldung "Der Verein hat noch Mannschaften in den Gruppen …" beim Löschen eines Vereins | Ein Verein kann nur gelöscht werden, wenn keine seiner Mannschaften mehr einer Gruppe zugeordnet ist; es wird nichts gelöscht. Die Meldung nennt die betroffenen Gruppen. Löschen Sie zunächst die Mannschaften des Vereins aus diesen Gruppen und danach den Verein (siehe [4. Manuelle Eingabe: Vereinstabelle](04_manuelle_eingabe.md#linke-spalte-vereinstabelle)). |
| Schaltfläche **+ Team** ist deaktiviert (Tooltip "Gruppe voll — erst Raster erhöhen") | Die Gruppe enthält bereits so viele Mannschaften, wie ihre Rastergröße zulässt; es kann keine weitere Mannschaft hinzugefügt werden. Erhöhen Sie zunächst die Rastergröße der Gruppe (siehe [4. Manuelle Eingabe: Gruppentabelle](04_manuelle_eingabe.md#linke-spalte-gruppentabelle)) und fügen Sie die Mannschaft anschließend hinzu. |
| Fehlermeldung "Ungültige Eingabe: Der Teamname muss mit dem Vereinsnamen (…) beginnen!" beim Umbenennen einer Mannschaft | Der neue Teamname beginnt nicht mit dem Namen des Vereins; die Eingabe wird automatisch zurückgesetzt. Behalten Sie den Vereinsnamen am Anfang des Teamnamens bei (z.B. "Borussia Duesseldorf II", siehe [4. Manuelle Eingabe: Mannschaftstabelle](04_manuelle_eingabe.md#rechte-spalte-mannschaftstabelle)). |
| Eingetragene Vereins-Schlüsselzahl wird nicht übernommen | Die Zahl liegt außerhalb des Referenzrasters der Spielwoche (also nicht zwischen 1 und der Rastergröße des Referenzrasters A/B bzw. X/Y) und wird abgewiesen. Tragen Sie eine Zahl aus diesem Bereich ein (siehe [4. Manuelle Eingabe: Vereinstabelle](04_manuelle_eingabe.md#linke-spalte-vereinstabelle)). |
| Fehlermeldung beim Klick auf **Anwenden & zur Übersicht** auf der Seite "Konflikte auflösen" | Es gibt noch ungelöste Konflikte (eine Schlüsselzahl ist in einer Gruppe mehrfach vergeben, auch an eine Mannschaft außerhalb des Konflikts) oder eine Mannschaft hat keine Schlüsselzahl. Die Meldung nennt die betroffenen Mannschaften, und die Seite bleibt geöffnet. Weisen Sie die Schlüsselzahlen so zu, dass jede Mannschaft eine Schlüsselzahl hat und jede Schlüsselzahl in der Gruppe nur einmal vergeben ist, und klicken Sie erneut auf **Anwenden & zur Übersicht** (siehe [6.1 Konflikte beheben](06_generierung.md#61-konflikte-beheben)). |
| Schaltfläche "Generieren" ist nicht aktiviert | Stellen Sie sicher, dass Daten geladen oder importiert wurden und die Übersicht geöffnet ist. |
| Schritt 2 ("Terminmeldung laden") auf der Datenimport-Seite ist nicht aktiviert | Stellen Sie sicher, dass zunächst eine CSV-Datei mit der Gruppenstruktur geladen wurde (Schritt 1 auf der Datenimport-Seite). Alternativ kann auch ein gespeicherter Zwischenstand über Schritt 3 ("Zwischenstand aus Datei laden") geladen werden, ohne dass Schritt 1 abgeschlossen sein muss. |
| Fehlermeldung "Das Referenzraster … wird von der aktuellen Konfiguration nicht unterstützt" beim Laden einer Datei | Die Datei verwendet ein Referenzraster, für das die aktuelle Konfiguration keinen Spielplan enthält. Importieren Sie die Konfiguration, mit der die Datei erstellt wurde (siehe [7.4 Konfiguration exportieren und importieren](07_sonstige_funktionen.md#74-konfiguration-exportieren-und-importieren)), und laden Sie die Datei erneut. |
| Fehlermeldung "Die Konfiguration ist ungültig" beim Konfigurationsimport | Die geladene Konfigurationsdatei enthält ungültige Werte (z.B. leere Altersklassen, widersprüchliche Rastergrößen oder fehlerhafte Spielplan-Einträge). Die Fehlermeldung listet die konkreten Verstöße auf. Exportieren Sie über die Seitenleiste → **Konfiguration exportieren** eine gültige Konfiguration und verwenden Sie diese als Vorlage. |
| Daten nach Seiten-Neuladen verloren | Die Browserdaten wurden gelöscht (oder ein anderer Browser bzw. ein privates Fenster verwendet). Laden Sie künftig regelmäßig einen Zwischenstand über **Speichern** als `Data.json` herunter, damit die Daten dauerhaft gesichert sind. |

---

[← 8. Typischer Arbeitsablauf](08_arbeitsablauf.md) | [Inhaltsverzeichnis](README.md) | [10. Seitennavigation →](10_navigation.md)
