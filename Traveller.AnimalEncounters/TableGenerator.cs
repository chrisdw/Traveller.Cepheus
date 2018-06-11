// animal v0.2 - a Traveller encounter table generator
// by D.S.Lewis 16/10/2007

// tables are based on LBB3 encounter rules

// usage: A>ANIMAL           generates (2-die table, earthlike)
//        A>ANIMAL /1        generates (1-die table, earthlike)
//        A>ANIMAL /2        generates (2-die table, earthlike)
//        A>ANIMAL 57 /1     generates (1 die table, size=5, atmo=7)

// todo list:
// herbivore behaviour should be FAS
// create an ecological assessment of each habitat - food chain & quantity

// version history:
// v0.2 - extracted the constant table data to a header file
//		- separated the Gen functions from Print functions, with an array to store critters
//		- scan the array of critters to find related ones across different regions
// v0.1 - basic implementation of the rules - just prints a tableusing System;using System;
using org.DownesWard.Traveller.Shared;
using org.DownesWard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace org.DownesWard.Traveller.AnimalEncounters
{
    public class TableGenerator
    {
        /* global arrays to store the critters */
        public const int MAX_CRIT = 264;
        readonly int[] cr_size = new int[MAX_CRIT];
        readonly int[] cr_type = new int[MAX_CRIT];
        readonly int[] cr_weapon = new int[MAX_CRIT];
        readonly int[] cr_armour = new int[MAX_CRIT];
        readonly int[] cr_attrib = new int[MAX_CRIT];
        readonly int[] cr_attack = new int[MAX_CRIT];
        readonly int[] cr_flee = new int[MAX_CRIT];
        readonly int[] cr_speed = new int[MAX_CRIT];
        readonly int[] cr_family = new int[MAX_CRIT];
        int cr_count = 0; // number of critters in the array

        int _tsize;
        UWP _upp;

        public Encounters Generate(int tsize, UWP uwp)
        {
            _tsize = tsize;
            _upp = uwp;

            GenerateTable(tsize, uwp);
            FindFamily(0);
            //prtTable(tsize, upp);
            return GetEncounters(tsize, uwp);
        }

        //---------------------------------------------------------------------------//
        // generate 1- or 2-dice encounter tables
        //---------------------------------------------------------------------------//
        protected void GenerateTable(int dice, UWP uwp)
        {
            GenerateRegion(dice, "Clear, Road, Open", uwp, 3, 0, 0);
            GenerateRegion(dice, "Prairie, Plain, Steppe", uwp, 4, 0, 0);
            GenerateRegion(dice, "Rough, Hills, Foothills", uwp, 0, 0, 0);
            GenerateRegion(dice, "Broken, Badlands", uwp, -3, -3, 0);
            GenerateRegion(dice, "Mountain, Alpine", uwp, 0, 0, 0);
            GenerateRegion(dice, "Forest, Woods", uwp, -4, -4, 0);
            GenerateRegion(dice, "Jungle, Rainforest", uwp, -3, -2, 0);
            GenerateRegion(dice, "River, Stream, Creek", uwp, 1, 1, 3);
            GenerateRegion(dice, "Swamp, Bog", uwp, -2, 4, 5);
            GenerateRegion(dice, "Marsh, Wetland", uwp, 0, -1, 2);
            GenerateRegion(dice, "Desert, Dunes", uwp, 3, -3, 0);
            GenerateRegion(dice, "Beach, Shore, Sea Edge", uwp, 3, 2, 1);
            GenerateRegion(dice, "Surface, Ocean, Sea", uwp, 2, 3, 4);
            GenerateRegion(dice, "Shallows, Ocean, Sea", uwp, 2, 2, 4);
            if (uwp.Hydro.Value > 0)
            {
                GenerateRegion(dice, "Depths, Ocean, Sea", uwp, 2, 4, 4);
                GenerateRegion(dice, "Bottom, Ocean, Sea", uwp, -4, 0, 4);
            }
            GenerateRegion(dice, "Sea Cave, Sea Cavern", uwp, -2, 0, 4);
            GenerateRegion(dice, "Sargasso, Seaweed", uwp, -4, -2, 4);
            GenerateRegion(dice, "Ruins, Old City", uwp, -3, 0, 0);
            GenerateRegion(dice, "Cave, Cavern", uwp, -4, 1, 0);
            GenerateRegion(dice, "Chasm, Crevass. Abyss", uwp, -1, -3, 0);
            GenerateRegion(dice, "Crater, Hollow", uwp, 0, -1, 0);

        }

        //---------------------------------------------------------------------------//
        // generate 1- or 2-dice encounter tables for a named region
        //---------------------------------------------------------------------------//
        protected void GenerateRegion(int dice, string name, UWP uwp, int typeDM, int sizeDM, int special)
        {
            //printf("\n%-60s UPP %-6s\n",name,upp);
            //printf("Die  Animal              Weight  Hits  Armour   Weapons           Wounds\n");
            if (dice == 1)
            {
                GenerateCritter(1, "S", uwp, typeDM, sizeDM, special);
                GenerateCritter(2, "H", uwp, typeDM, sizeDM, special);
                GenerateCritter(3, "H", uwp, typeDM, sizeDM, special);
                GenerateCritter(4, "H", uwp, typeDM, sizeDM, special);
                GenerateCritter(5, "O", uwp, typeDM, sizeDM, special);
                GenerateCritter(6, "C", uwp, typeDM, sizeDM, special);
            }
            else
            {
                GenerateCritter(2, "S", uwp, typeDM, sizeDM, special);
                GenerateCritter(3, "O", uwp, typeDM, sizeDM, special);
                GenerateCritter(4, "S", uwp, typeDM, sizeDM, special);
                GenerateCritter(5, "O", uwp, typeDM, sizeDM, special);
                GenerateCritter(6, "H", uwp, typeDM, sizeDM, special);
                GenerateCritter(7, "H", uwp, typeDM, sizeDM, special);
                GenerateCritter(8, "H", uwp, typeDM, sizeDM, special);
                GenerateCritter(9, "C", uwp, typeDM, sizeDM, special);
                GenerateCritter(10, "E", uwp, typeDM, sizeDM, special);
                GenerateCritter(11, "C", uwp, typeDM, sizeDM, special);
                GenerateCritter(12, "C", uwp, typeDM, sizeDM, special);
            }
        }

        //---------------------------------------------------------------------------//
        // generate a single critter, by type
        //---------------------------------------------------------------------------//
        protected void GenerateCritter(int dnum, string ctype, UWP uwp, int typeDM, int sizeDM, int special)
        {
            // other attributes of a critter
            int size = 0;
            int weapon = 0;
            int armour = 0;
            int attrib = 0; // flyer etc
            int att_roll = Roll(6) + Roll(6);
            int attack;
            int flee;
            int speed;

            // choose a specific type within the overall class
            int type = Roll(6) + Roll(6);

            // apply DMs
            type += typeDM;
            if (type < 0) type = 0;
            if (type > 13) type = 13;

            // add offset for overall class
            // no offset for herbivores
            if (ctype[0] == 'O') type += 14;
            if (ctype[0] == 'C') type += 28;
            if (ctype[0] == 'S') type += 42;
            if (ctype[0] == 'E') type += 56;

            // check for other attributes
            // adjust roll for planet-type
            if (uwp.Size.Value > 8) att_roll -= 1;
            if (uwp.Size.Value < 6) att_roll += 1;
            if (uwp.Size.Value > 4) att_roll += 1; // cumulative with the above for +2
            if (uwp.Atmosphere.Value > 7) att_roll += 1;
            if (uwp.Atmosphere.Value < 6) att_roll -= 1;
            if (att_roll < 2) att_roll = 2;
            if (att_roll > 12) att_roll = 12;

            if (special == 0)
            { // general terrain
                if (att_roll == 10) { attrib = 2; sizeDM = -6; }
                if (att_roll == 11) { attrib = 2; sizeDM = -5; }
                if (att_roll == 12) { attrib = 2; sizeDM = -3; }
            }
            if (special == 1)
            { // beach
                if (att_roll == 2) { attrib = 3; sizeDM += 1; }
                if (att_roll == 3) { attrib = 1; sizeDM += 2; }
                if (att_roll == 4) { attrib = 1; sizeDM += 2; }
                if (att_roll == 11) { attrib = 2; sizeDM = -6; }
                if (att_roll == 12) { attrib = 2; sizeDM = -5; }
            }
            if (special == 2)
            { // marsh
                if (att_roll == 2) { attrib = 3; sizeDM -= 6; }
                if (att_roll == 3) { attrib = 1; sizeDM += 2; }
                if (att_roll == 4) { attrib = 1; sizeDM += 1; }
                if (att_roll == 11) { attrib = 2; sizeDM = -6; }
                if (att_roll == 12) { attrib = 2; sizeDM = -5; }
            }
            if (special == 3)
            { // river
                if (att_roll == 2) { attrib = 3; sizeDM += 1; }
                if (att_roll == 3) { attrib = 1; sizeDM += 1; }
                if (att_roll == 11) { attrib = 2; sizeDM = -6; }
                if (att_roll == 12) { attrib = 2; sizeDM = -5; }
            }
            if (special == 4)
            { // sea
                if (att_roll == 2) { attrib = 3; sizeDM += 2; }
                if (att_roll == 3) { attrib = 3; sizeDM += 2; }
                if (att_roll == 4) { attrib = 3; sizeDM += 2; }
                if (att_roll == 5) { attrib = 1; sizeDM += 2; }
                if (att_roll == 6) { attrib = 1; sizeDM += 0; }
                if (att_roll == 7) { attrib = 3; sizeDM += 1; }
                if (att_roll == 8) { attrib = 3; sizeDM -= 1; }
                if (att_roll == 9) { attrib = 4; sizeDM -= 7; }
                if (att_roll == 10) { attrib = 4; sizeDM -= 6; }
                if (att_roll == 11) { attrib = 2; sizeDM = -6; }
                if (att_roll == 12) { attrib = 2; sizeDM = -5; }
            }
            if (special == 5)
            { // swamp
                if (att_roll == 2) { attrib = 3; sizeDM -= 3; }
                if (att_roll == 3) { attrib = 1; sizeDM += 1; }
                if (att_roll == 4) { attrib = 1; sizeDM += 1; }
                if (att_roll == 11) { attrib = 2; sizeDM = -6; }
                if (att_roll == 12) { attrib = 2; sizeDM = -5; }
            }

            // adjust size for planet
            if (uwp.Size.Value > 7) sizeDM -= 1;
            if (uwp.Size.Value < 5) sizeDM += 1;


            // derive a size
            size = Roll(6) + Roll(6) + sizeDM;
            if (size < 1) size = 1;
            if (size > 20) size = 20;
            while (TableData.weights[size][0] == '*')
            {
                size = Roll(6) + Roll(6) + sizeDM + 6;
                if (size < 1) size = 1;
                if (size > 20) size = 20;
            }

            if (ctype[0] == 'E') size = 0;


            // derive armour
            armour = Roll(6) + Roll(6);
            if (ctype[0] == 'C') armour -= 1;
            if (ctype[0] == 'S') armour += 1;
            if (ctype[0] == 'H') armour += 2;
            if (armour < 1) armour = 1;
            if (armour > 20) armour = 20;
            while (TableData.armours[armour][0] == '*')
            {
                armour = Roll(6) + Roll(6) + 6;
                if (ctype[0] == 'C') armour -= 1;
                if (ctype[0] == 'S') armour += 1;
                if (ctype[0] == 'H') armour += 2;
                if (armour < 1) armour = 1;
                if (armour > 20) armour = 20;
            }

            if (ctype[0] == 'E') armour = 0;

            // derive weapons
            weapon = Roll(6) + Roll(6);
            if (ctype[0] == 'O') weapon += 4;
            if (ctype[0] == 'C') weapon += 8;
            if (ctype[0] == 'H') weapon -= 3;
            if (weapon < 1) weapon = 1;
            if (weapon > 20) weapon = 20;

            if (ctype[0] == 'E') weapon = 0;

            // derive behaviours
            attack = Roll(6); flee = Roll(6); speed = Roll(6); // defaults
            if ((TableData.ctypes[type])[0] == 'F')
            { // filter
                attack = 0; flee += 2; speed -= 5;
            }
            if ((TableData.ctypes[type])[0] == 'I' && ctype[0] == 'H')
            { // intermittent
                attack += 3; flee += 3; speed -= 4;
            }
            if ((TableData.ctypes[type])[0] == 'G' && ctype[0] == 'H')
            { // grazer
                attack += 2; flee += 0; speed -= 2;
            }
            if ((TableData.ctypes[type])[0] == 'G' && ctype[0] == 'O')
            { // gatherer
                attack += 3; flee += 2; speed -= 3;
            }
            if ((TableData.ctypes[type])[0] == 'H' && ctype[0] == 'O')
            { // hunter
                attack += 0; flee += 2; speed -= 4;
            }
            if ((TableData.ctypes[type])[0] == 'E')
            { // eater
                attack += 0; flee += 3; speed -= 3;
            }
            if ((TableData.ctypes[type])[0] == 'P')
            { // pouncer
                attack = 0; flee = 0; speed -= 4;
            }
            if ((TableData.ctypes[type])[0] == 'C' && ctype[0] == 'C')
            { // chaser
                attack = 0; flee += 3; speed -= 2;
            }
            if ((TableData.ctypes[type])[0] == 'T')
            { // trapper
                attack = 0; flee += 2; speed -= 5;
            }
            if ((TableData.ctypes[type])[0] == 'S')
            { // siren
                attack = 0; flee += 3; speed -= 4;
            }
            if ((TableData.ctypes[type])[0] == 'K')
            { // killer
                attack += 0; flee += 3; speed -= 3;
            }
            if ((TableData.ctypes[type])[0] == 'H' && ctype[0] == 'S')
            { // hijacker
                attack += 1; flee += 2; speed -= 4;
            }
            if ((TableData.ctypes[type])[0] == 'I' && ctype[0] == 'S')
            { // intimidater
                attack += 2; flee += 1; speed -= 4;
            }
            if ((TableData.ctypes[type])[0] == 'C' && ctype[0] == 'S')
            { // carrion-eater
                attack += 3; flee += 2; speed -= 3;
            }
            if ((TableData.ctypes[type])[0] == 'R' && ctype[0] == 'S')
            { // reducer
                attack += 3; flee += 2; speed -= 4;
            }

            if (speed < 0) speed = 0;

            // output the critter to the table
            cr_size[cr_count] = size;
            cr_type[cr_count] = type;
            cr_weapon[cr_count] = weapon + TableData.weapon_dups[weapon]; // normalise repeated weapons to a single index
            cr_armour[cr_count] = armour;
            cr_attrib[cr_count] = attrib;
            cr_attack[cr_count] = attack;
            cr_flee[cr_count] = flee;
            cr_speed[cr_count] = speed;
            cr_family[cr_count] = 0;
            if (cr_count < MAX_CRIT) cr_count += 1; // increment counter (if possible)
        }


        public void WriteStreamAsText(TextWriter sw)
        {
            PrintTable(sw, _tsize, _upp);
        }

        public void WriteToXML(System.Xml.XmlNode parent)
        {
            // initialise counter to iterate through the arrays

            cr_count = 0;
            int dice = _tsize;
            UWP upp = _upp;

            XmlRegion(parent, dice, "Clear, Road, Open", upp);
            XmlRegion(parent, dice, "Prairie, Plain, Steppe", upp);
            XmlRegion(parent, dice, "Rough, Hills, Foothills", upp);
            XmlRegion(parent, dice, "Broken, Badlands", upp);
            XmlRegion(parent, dice, "Mountain, Alpine", upp);
            XmlRegion(parent, dice, "Forest, Woods", upp);
            XmlRegion(parent, dice, "Jungle, Rainforest", upp);
            XmlRegion(parent, dice, "River, Stream, Creek", upp);
            XmlRegion(parent, dice, "Swamp, Bog", upp);
            XmlRegion(parent, dice, "Marsh, Wetland", upp);
            XmlRegion(parent, dice, "Desert, Dunes", upp);
            XmlRegion(parent, dice, "Beach, Shore, Sea Edge", upp);
            XmlRegion(parent, dice, "Surface, Ocean, Sea", upp);
            XmlRegion(parent, dice, "Shallows, Ocean, Sea", upp);
            if (upp.Hydro.Value > 0)
            {
                XmlRegion(parent, dice, "Depths, Ocean, Sea", upp);
                XmlRegion(parent, dice, "Bottom, Ocean, Sea", upp);
            }
            XmlRegion(parent, dice, "Sea Cave, Sea Cavern", upp);
            XmlRegion(parent, dice, "Sargasso, Seaweed", upp);
            XmlRegion(parent, dice, "Ruins, Old City", upp);
            XmlRegion(parent, dice, "Cave, Cavern", upp);
            XmlRegion(parent, dice, "Chasm, Crevass. Abyss", upp);
            XmlRegion(parent, dice, "Crater, Hollow", upp);
        }

        private void XmlRegion(System.Xml.XmlNode parent, int dice, string name, UWP uwp)
        {
            System.Xml.XmlElement region = parent.OwnerDocument.CreateElement("Region");
            System.Xml.XmlElement nameEle = parent.OwnerDocument.CreateElement("name");
            nameEle.InnerText = name;
            region.AppendChild(nameEle);

            if (dice == 1)
            {
                XmlCritter(region, 1);
                XmlCritter(region, 2);
                XmlCritter(region, 3);
                XmlCritter(region, 4);
                XmlCritter(region, 5);
                XmlCritter(region, 6);
            }
            else
            {
                XmlCritter(region, 2);
                XmlCritter(region, 3);
                XmlCritter(region, 4);
                XmlCritter(region, 5);
                XmlCritter(region, 6);
                XmlCritter(region, 7);
                XmlCritter(region, 8);
                XmlCritter(region, 9);
                XmlCritter(region, 10);
                XmlCritter(region, 11);
                XmlCritter(region, 12);
            }
            parent.AppendChild(region);
        }

        private void XmlCritter(System.Xml.XmlElement region, int dnum)
        {
            System.Xml.XmlElement critter = region.OwnerDocument.CreateElement("critter");

            System.Xml.XmlAttribute ctype = region.OwnerDocument.CreateAttribute("type");
            ctype.Value = cr_type[cr_count].ToString();
            critter.Attributes.Append(ctype);
            System.Xml.XmlAttribute dnumAttr = region.OwnerDocument.CreateAttribute("dnum");
            dnumAttr.Value = dnum.ToString();
            critter.Attributes.Append(dnumAttr);

            if (TableData.attribs[cr_attrib[cr_count]].ToString().Trim().Length > 0)
            {
                System.Xml.XmlAttribute attribAttr = region.OwnerDocument.CreateAttribute("attribute");
                attribAttr.Value = TableData.attribs[cr_attrib[cr_count]].ToString();
                critter.Attributes.Append(attribAttr);
            }
            System.Xml.XmlElement ctypeDescr = region.OwnerDocument.CreateElement("description");
            ctypeDescr.InnerText = TableData.ctypes[cr_type[cr_count]].Trim();
            critter.AppendChild(ctypeDescr);

            if (cr_type[cr_count] <= 55)
            {
                System.Xml.XmlElement ctypeWeight = region.OwnerDocument.CreateElement("weight");
                ctypeWeight.InnerText = TableData.weights[cr_size[cr_count]];
                critter.AppendChild(ctypeWeight);

                System.Xml.XmlElement ctypeArmour = region.OwnerDocument.CreateElement("armour");
                ctypeArmour.InnerText = TableData.armours[cr_armour[cr_count]];
                critter.AppendChild(ctypeArmour);

                System.Xml.XmlElement ctypeWeapons = region.OwnerDocument.CreateElement("weapons");
                ctypeWeapons.InnerText = TableData.weapons[cr_weapon[cr_count]];
                critter.AppendChild(ctypeWeapons);

                if (TableData.wounds[cr_size[cr_count]].Trim().Length > 0)
                {
                    System.Xml.XmlElement ctypeWounds = region.OwnerDocument.CreateElement("wounds");
                    ctypeWounds.InnerText = TableData.wounds[cr_size[cr_count]];
                    critter.AppendChild(ctypeWounds);
                }

                System.Xml.XmlElement ctypeAttack = region.OwnerDocument.CreateElement("attack");
                ctypeAttack.InnerText = cr_attack[cr_count].ToString();
                critter.AppendChild(ctypeAttack);

                System.Xml.XmlElement ctypeFlee = region.OwnerDocument.CreateElement("flee");
                ctypeFlee.InnerText = cr_flee[cr_count].ToString();
                critter.AppendChild(ctypeFlee);

                System.Xml.XmlElement ctypeSpeed = region.OwnerDocument.CreateElement("speed");
                ctypeSpeed.InnerText = cr_speed[cr_count].ToString();
                critter.AppendChild(ctypeSpeed);
            }

            if (cr_family[cr_count] > 0)
            { // critter is part of a family
                System.Xml.XmlAttribute familyAttr = region.OwnerDocument.CreateAttribute("family");
                familyAttr.Value = cr_family[cr_count].ToString();
                critter.Attributes.Append(familyAttr);
            }

            region.AppendChild(critter);
            if (cr_count < MAX_CRIT) cr_count += 1; // increment counter (if possible)
        }

        //---------------------------------------------------------------------------//
        // output 1- or 2-dice encounter tables
        //---------------------------------------------------------------------------//
        protected void PrintTable(TextWriter sw, int dice, UWP uwp)
        {
            // initialise counter to iterate through the arrays

            cr_count = 0;

            PrintRegion(sw, dice, "Clear, Road, Open", uwp);
            PrintRegion(sw, dice, "Prairie, Plain, Steppe", uwp);
            PrintRegion(sw, dice, "Rough, Hills, Foothills", uwp);
            PrintRegion(sw, dice, "Broken, Badlands", uwp);
            PrintRegion(sw, dice, "Mountain, Alpine", uwp);
            PrintRegion(sw, dice, "Forest, Woods", uwp);
            PrintRegion(sw, dice, "Jungle, Rainforest", uwp);
            PrintRegion(sw, dice, "River, Stream, Creek", uwp);
            PrintRegion(sw, dice, "Swamp, Bog", uwp);
            PrintRegion(sw, dice, "Marsh, Wetland", uwp);
            PrintRegion(sw, dice, "Desert, Dunes", uwp);
            PrintRegion(sw, dice, "Beach, Shore, Sea Edge", uwp);
            PrintRegion(sw, dice, "Surface, Ocean, Sea", uwp);
            PrintRegion(sw, dice, "Shallows, Ocean, Sea", uwp);
            if (uwp.Hydro.Value > 0)
            {
                PrintRegion(sw, dice, "Depths, Ocean, Sea", uwp);
                PrintRegion(sw, dice, "Bottom, Ocean, Sea", uwp);
            }
            PrintRegion(sw, dice, "Sea Cave, Sea Cavern", uwp);
            PrintRegion(sw, dice, "Sargasso, Seaweed", uwp);
            PrintRegion(sw, dice, "Ruins, Old City", uwp);
            PrintRegion(sw, dice, "Cave, Cavern", uwp);
            PrintRegion(sw, dice, "Chasm, Crevass. Abyss", uwp);
            PrintRegion(sw, dice, "Crater, Hollow", uwp);

        }

        //---------------------------------------------------------------------------//
        // output 1- or 2-dice encounter tables for a named region
        //---------------------------------------------------------------------------//
        protected void PrintRegion(TextWriter sw, int dice, string name, UWP uwp)
        {
            sw.WriteLine();
            sw.WriteLine("{0,65} UPP {1,6}", name, uwp.PhysicalUWP());
            sw.WriteLine("Die  Animal              Weight  Hits  Armour   Weapons           Wounds");
            if (dice == 1)
            {
                PrintCritter(sw, 1);
                PrintCritter(sw, 2);
                PrintCritter(sw, 3);
                PrintCritter(sw, 4);
                PrintCritter(sw, 5);
                PrintCritter(sw, 6);
            }
            else
            {
                PrintCritter(sw, 2);
                PrintCritter(sw, 3);
                PrintCritter(sw, 4);
                PrintCritter(sw, 5);
                PrintCritter(sw, 6);
                PrintCritter(sw, 7);
                PrintCritter(sw, 8);
                PrintCritter(sw, 9);
                PrintCritter(sw, 10);
                PrintCritter(sw, 11);
                PrintCritter(sw, 12);
            }
        }

        //---------------------------------------------------------------------------//
        // output a single critter, by array index
        // the critter to output is indexed by the global int cr_count
        // and this function increments the index ready for the next call
        //---------------------------------------------------------------------------//
        protected void PrintCritter(TextWriter sw, int dnum)
        {

            // print the critter to the output stream
            if (cr_type[cr_count] < 14)
            { // special treatment for herbivores - FAS behaviour
                sw.Write("{0,2} {1} {2} {3} {4} {5}{6} F{7}A{8}S{9}",
                    dnum, TableData.attribs[cr_attrib[cr_count]], TableData.ctypes[cr_type[cr_count]],
                    TableData.weights[cr_size[cr_count]], TableData.armours[cr_armour[cr_count]],
                    TableData.weapons[cr_weapon[cr_count]], TableData.wounds[cr_size[cr_count]],
                    cr_flee[cr_count], cr_attack[cr_count], cr_speed[cr_count]);
            }
            else if (cr_type[cr_count] > 55)
            {
                // Event
                sw.Write("{0,2} {1} {2}",
                    dnum, TableData.attribs[cr_attrib[cr_count]], TableData.ctypes[cr_type[cr_count]]);
            }
            else
            {
                sw.Write("{0,2} {1} {2} {3} {4} {5}{6} A{7}F{8}S{9}",
                    dnum, TableData.attribs[cr_attrib[cr_count]], TableData.ctypes[cr_type[cr_count]],
                    TableData.weights[cr_size[cr_count]], TableData.armours[cr_armour[cr_count]],
                    TableData.weapons[cr_weapon[cr_count]], TableData.wounds[cr_size[cr_count]],
                    cr_attack[cr_count], cr_flee[cr_count], cr_speed[cr_count]);
            }

            if (cr_family[cr_count] > 0)
            { // critter is part of a family
                sw.Write(" *{0,2}", cr_family[cr_count]);
            }

            sw.WriteLine();

            if (cr_count < MAX_CRIT) cr_count += 1; // increment counter (if possible)

        }


        //---------------------------------------------------------------------------//
        // roll a die of the specified shape
        //---------------------------------------------------------------------------//
        int Roll(int sides)
        {
            var die = new Dice(sides);

            int dieRoll = die.roll();
            return (dieRoll);
        }

        //---------------------------------------------------------------------------//
        // find similar critters in the arrays and set their family indicators
        //---------------------------------------------------------------------------//
        protected void FindFamily(int cr_num)
        {
            int fam_count = 0; // used to set next family indicator
            int cr_xref = 0;   // 2nd array index to iterate critters to compare with cr_num

            // for each critter in the array
            for (cr_num = 0; cr_num < cr_count; cr_num++)
            {                for (cr_xref = cr_num + 1; cr_xref <= cr_count; cr_xref++)
                {
                    if ((cr_type[cr_num] < 56) && // not applicable to events (only critters)
                       (cr_type[cr_num] == cr_type[cr_xref]) && // same behaviour
                       (cr_weapon[cr_num] == cr_weapon[cr_xref]) && // same weapon
                       (cr_attrib[cr_num] == cr_attrib[cr_xref]) && // same locomotion
                       (Math.Abs(cr_size[cr_num] - cr_size[cr_xref]) < 2))
                    { // similar size
                        if (cr_family[cr_num] == 0)
                        { // not part of a family yet
                            fam_count++; // assign next family number
                            cr_family[cr_num] = fam_count;
                        }
                        cr_family[cr_xref] = cr_family[cr_num]; // join xref to same family
                    }
                }
            }
        }

        private Encounters GetEncounters(int tsize, UWP uwp)
        {
            var encounters = new Encounters();
            cr_count = 0;
            encounters.Regions.Add(GetRegion(tsize, "Clear, Road, Open"));
            encounters.Regions.Add(GetRegion(tsize, "Prairie, Plain, Steppe"));
            encounters.Regions.Add(GetRegion(tsize, "Rough, Hills, Foothills"));
            encounters.Regions.Add(GetRegion(tsize, "Broken, Badlands"));
            encounters.Regions.Add(GetRegion(tsize, "Mountain, Alpine"));
            encounters.Regions.Add(GetRegion(tsize, "Forest, Woods"));
            encounters.Regions.Add(GetRegion(tsize, "Jungle, Rainforest"));
            encounters.Regions.Add(GetRegion(tsize, "River, Stream, Creek"));
            encounters.Regions.Add(GetRegion(tsize, "Swamp, Bog"));
            encounters.Regions.Add(GetRegion(tsize, "Marsh, Wetland"));
            encounters.Regions.Add(GetRegion(tsize, "Desert, Dunes"));
            encounters.Regions.Add(GetRegion(tsize, "Beach, Shore, Sea Edge"));
            encounters.Regions.Add(GetRegion(tsize, "Surface, Ocean, Sea"));
            encounters.Regions.Add(GetRegion(tsize, "Shallows, Ocean, Sea"));
            if (uwp.Hydro.Value > 0)
            {
                encounters.Regions.Add(GetRegion(tsize, "Depths, Ocean, Sea"));
                encounters.Regions.Add(GetRegion(tsize, "Bottom, Ocean, Sea"));
            }
            encounters.Regions.Add(GetRegion(tsize, "Sea Cave, Sea Cavern"));
            encounters.Regions.Add(GetRegion(tsize, "Sargasso, Seaweed"));
            encounters.Regions.Add(GetRegion(tsize, "Ruins, Old City"));
            encounters.Regions.Add(GetRegion(tsize, "Cave, Cavern"));
            encounters.Regions.Add(GetRegion(tsize, "Chasm, Crevass. Abyss"));
            encounters.Regions.Add(GetRegion(tsize, "Crater, Hollow"));

            return encounters;
        }

        private Region GetRegion(int dice, string name)
        {
            var region = new Region()
            {
                Name = name
            };
            var min = 2;
            var max = 12;

            if (dice == 1)
            {
                min = 1;
                max = 6;
            }
            for (var i = min; i <= max; i++)
            {
                region.Critters.Add(GetCritter(i));
            }
            return region;
        }

        private Critter GetCritter(int dicenum)
        {
            var critter = new Critter()
            {
                Dienum = dicenum,
                Armour = TableData.armours[cr_armour[cr_count]],
                Attack = cr_attack[cr_count],
                Attribute = TableData.attribs[cr_attrib[cr_count]].ToString(),
                CritterType = cr_type[cr_count],
                Family = cr_family[cr_count],
                Flee = cr_flee[cr_count],
                Speed = cr_speed[cr_count],
                Weapons = TableData.weapons[cr_weapon[cr_count]].Trim(),
                WeaponsDamage = TableData.weaponsDmg[cr_weapon[cr_count]],
                WeaponsDesc = TableData.weaponsDesc[cr_weapon[cr_count]],
                Weight = TableData.weights[cr_size[cr_count]].Trim(),
                WeightKg = TableData.weightsKg[cr_size[cr_count]],
                WeightHits = TableData.weightsHits[cr_size[cr_count]],
                Wounds = TableData.wounds[cr_size[cr_count]],
            };

            if (cr_count < MAX_CRIT)
            {
                cr_count++; // increment counter (if possible)
            }
            return critter;
        }
    }
}
