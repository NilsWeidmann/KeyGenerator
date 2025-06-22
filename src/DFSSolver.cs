using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KeyGenerator
{
    public class DFSSolver
    {
        private int[] field;
        private Tuple<int, bool>[] prio;
        private int currentConflicts;
        private int runtime;
        private KeyMapper km;
        private TextFile log;
        private List<String> notification;
        public DFSSolver(int[] field, Tuple<int, bool>[] prio, int currentConflicts, int runtime, KeyMapper km, TextFile log, List<String> notification)
        {
            this.field = field;
            this.prio = prio;
            this.currentConflicts = currentConflicts;
            this.runtime = runtime;
            this.km = km;
            this.log = log;
            this.notification = notification;
        }

        public void findSolution(Group[] l, Group[] best_l, Club[] c, Club[] best_c, int[] conflicts, int[] key, BackgroundWorker bw)
        {
            DateTime start = DateTime.Now;
            int progress = 0;
            HashSet<string> ht = new HashSet<string>();
            string value;
            int pointer = -1, idx;
            Club club;
            char week1, week2;
            int[] rand = {  (int)(new Random()).NextDouble() * field[0],
                        (int)(new Random()).NextDouble() * field[1]};

            while (!ht.Contains("") && prio.Length > 0)
            {
                if (++pointer == c.Length * 2)
                {
                    setAdditional(l, best_l, c, best_c, conflicts, key);
                    writeLog(start, currentConflicts);
                    safeAdd(--pointer, key, prio, ht);
                }
                else
                {
                    club = c[prio[pointer].Item1];
                    idx = prio[pointer].Item2 ? 0 : 1;
                    week1 = prio[pointer].Item2 ? 'A' : 'X';
                    week2 = prio[pointer].Item2 ? 'B' : 'Y';

                    if (club.prio[idx] == 0)
                    {
                        setAdditional(l, best_l, c, best_c, conflicts, key);
                        writeLog(start, currentConflicts);
                        safeAdd(--pointer, key, prio, ht);
                    }
                    else
                    {
                        do
                        {
                            rand[idx] %= field[idx];
                            key[pointer] = ++rand[idx];
                            value = getValue(key);
                        } while (ht.Contains(value));

                        assignKey(club, week1, week2, key, pointer, idx);

                        if (Data.checkFatal(l, conflicts, club) != null)
                            safeAdd(pointer, key, prio, ht);
                        else
                            continue;
                    }
                }

                // Zurücksetzen
                conflicts[0] = -1;
                for (; pointer >= 0; pointer--)
                {
                    club = c[prio[pointer].Item1];
                    week1 = prio[pointer].Item2 ? 'A' : 'X';
                    week2 = prio[pointer].Item2 ? 'B' : 'Y';

                    key[pointer] = 0;
                    club.keys[week1] = club.keys[week2] = 0;
                    foreach (Team team in club.team)
                        if (team == null)
                            continue;
                        else if (team.week == week1 || team.week == week2)
                        {
                            team.key = 0;
                            team.group.getAllocation();
                        }
                }

                if (bw.CancellationPending)
                    return;
                else
                    progress = reportProgress(progress, start, bw);
            }
        }

        private void writeLog(DateTime start, int currentConflicts)
        {
            log.Append("\n" + System.DateTime.Now.ToLongTimeString() + ";" + System.DateTime.Now.Subtract(start).TotalSeconds + ";" + currentConflicts + ";", notification);
        }



        private void assignKey(Club club, char week1, char week2, int[] key, int p, int idx)
        {
            club.keys[week1] = key[p];
            club.keys[week2] = km.getOpposed(field[idx], key[p]);
            foreach (Team team in club.team)
                if (team == null)
                    continue;
                else if (team.week == week1)
                    team.key = km.getParallel(field[idx], team.group.field, club.keys[week1]).First();
                else if (team.week == week2)
                    team.key = km.getParallel(field[idx], team.group.field, club.keys[week2]).First();
        }

        private int reportProgress(int progress, DateTime start, BackgroundWorker bw)
        {
            if (progress < 100 * (int)(DateTime.Now - start).TotalSeconds / runtime)
            {
                progress = 100 * (int)(DateTime.Now - start).TotalSeconds / runtime;
                bw.ReportProgress(progress > 100 ? 100 : progress);
                if (progress >= 100)
                    bw.CancelAsync();
            }
            return progress;
        }

        public void setAdditional(Group[] l, Group[] best_l, Club[] club, Club[] best_club, int[] conflicts, int[] key)
        {
            for (int i = 0; i < l.Length; i++)
                for (int j = 0; j < l[i].nrOfTeams; j++)
                    if (l[i].team[j].key == 0 && l[i].team[j].week == '-' && l[i].team[j].hasAdditional())
                    {
                        for (int k = 0; k < l[i].field; k++)
                            if (l[i].team[j].keyOK(k + 1))
                            {
                                l[i].team[j].key = k + 1;
                                if (Data.checkFatal(l, conflicts, null) != null)
                                    conflicts[0] = -1;
                                else
                                    setAdditional(l, best_l, club, best_club, conflicts, key);
                            }
                        l[i].team[j].key = 0;
                        conflicts[0] = -1;
                        return;
                    }
            if (conflicts[1] == conflicts[0])
                return;
            // Neue beste Lösung wurde gefunden
            conflicts[1] = conflicts[0];
            currentConflicts = conflicts[0];
            Data.copy(l, best_l, club, best_club);
        }

        public void safeAdd(int pos, int[] keys, Tuple<int, bool>[] prio, HashSet<string> ht)
        {
            string value = getValue(keys);

            /*string[] values = ht.ToArray();
            foreach (string otherValue in values)
                if (otherValue.StartsWith(value))
                    ht.Remove(otherValue);*/

            ht.Add(value);

            if (pos < 0)
                return;

            int currentField = prio[pos].Item2 ? field[0] : field[1];

            for (int i = 1; i <= currentField; i++)
            {
                keys[pos] = i;
                value = getValue(keys);
                if (!ht.Contains(value))
                    return;
            }

            for (int i = 1; i <= currentField; i++)
            {
                keys[pos] = i;
                value = getValue(keys);
                ht.Remove(value);
            }
            keys[pos] = 0;

            safeAdd(pos - 1, keys, prio, ht);

            //fanoCheck(ht);
        }
        public static string getValue(int[] key)
        {
            string result = "";
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == 0)
                    break;
                if (key[i] < 10)
                    result += key[i];
                else
                    switch (key[i])
                    {
                        case 10: result += 'A'; break;
                        case 11: result += 'B'; break;
                        case 12: result += "C"; break;
                        case 13: result += "D"; break;
                        case 14: result += "E"; break;
                    }
            }
            return result;
        }
    }
}
