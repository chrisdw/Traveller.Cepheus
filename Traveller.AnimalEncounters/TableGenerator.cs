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

        int[] cr_size = new int[MAX_CRIT];
        int[] cr_type = new int[MAX_CRIT];
        int[] cr_weapon = new int[MAX_CRIT];
        int[] cr_armour = new int[MAX_CRIT];
        int[] cr_attrib = new int[MAX_CRIT];
        int[] cr_attack = new int[MAX_CRIT];
        int[] cr_flee = new int[MAX_CRIT];
        int[] cr_speed = new int[MAX_CRIT];
        int[] cr_family = new int[MAX_CRIT];
        int cr_count = 0; // number of critters in the array

        int _tsize;
        UPP _upp;

        public Encounters Generate(int tsize, UPP upp)
        {
            _tsize = tsize;
            _upp = upp;

            genTable(tsize, upp);
            findFamily(0);
            //prtTable(tsize, upp);
            return GetEncounters(tsize, upp);
        }

        //---------------------------------------------------------------------------//
        // generate 1- or 2-dice encounter tables
        //---------------------------------------------------------------------------//
        protected void genTable(int dice, UPP upp)
        {
            genRegion(dice, "Clear, Road, Open", upp, 3, 0, 0);
            genRegion(dice, "Prairie, Plain, Steppe", upp, 4, 0, 0);
            genRegion(dice, "Rough, Hills, Foothills", upp, 0, 0, 0);
            genRegion(dice, "Broken, Badlands", upp, -3, -3, 0);
            genRegion(dice, "Mountain, Alpine", upp, 0, 0, 0);
            genRegion(dice, "Forest, Woods", upp, -4, -4, 0);
            genRegion(dice, "Jungle, Rainforest", upp, -3, -2, 0);
            genRegion(dice, "River, Stream, Creek", upp, 1, 1, 3);
            genRegion(dice, "Swamp, Bog", upp, -2, 4, 5);
            genRegion(dice, "Marsh, Wetland", upp, 0, -1, 2);
            genRegion(dice, "Desert, Dunes", upp, 3, -3, 0);
            genRegion(dice, "Beach, Shore, Sea Edge", upp, 3, 2, 1);
            genRegion(dice, "Surface, Ocean, Sea", upp, 2, 3, 4);
            genRegion(dice, "Shallows, Ocean, Sea", upp, 2, 2, 4);
            if (upp.Hydro.Value > 0)
            {
                genRegion(dice, "Depths, Ocean, Sea", upp, 2, 4, 4);
                genRegion(dice, "Bottom, Ocean, Sea", upp, -4, 0, 4);
            }
            genRegion(dice, "Sea Cave, Sea Cavern", upp, -2, 0, 4);
            genRegion(dice, "Sargasso, Seaweed", upp, -4, -2, 4);
            genRegion(dice, "Ruins, Old City", upp, -3, 0, 0);
            genRegion(dice, "Cave, Cavern", upp, -4, 1, 0);
            genRegion(dice, "Chasm, Crevass. Abyss", upp, -1, -3, 0);
            genRegion(dice, "Crater, Hollow", upp, 0, -1, 0);

        }

        //---------------------------------------------------------------------------//
        // generate 1- or 2-dice encounter tables for a named region
        //---------------------------------------------------------------------------//
        protected void genRegion(int dice, string name, UPP upp, int typeDM, int sizeDM, int special)
        {
            //printf("\n%-60s UPP %-6s\n",name,upp);
            //printf("Die  Animal              Weight  Hits  Armour   Weapons           Wounds\n");
            if (dice == 1)
            {
                genCritter(1, "S", upp, typeDM, sizeDM, special);
                genCritter(2, "H", upp, typeDM, sizeDM, special);
                genCritter(3, "H", upp, typeDM, sizeDM, special);
                genCritter(4, "H", upp, typeDM, sizeDM, special);
                genCritter(5, "O", upp, typeDM, sizeDM, special);
                genCritter(6, "C", upp, typeDM, sizeDM, special);
            }
            else
            {
                genCritter(2, "S", upp, typeDM, sizeDM, special);
                genCritter(3, "O", upp, typeDM, sizeDM, special);
                genCritter(4, "S", upp, typeDM, sizeDM, special);
                genCritter(5, "O", upp, typeDM, sizeDM, special);
                genCritter(6, "H", upp, typeDM, sizeDM, special);
                genCritter(7, "H", upp, typeDM, sizeDM, special);
                genCritter(8, "H", upp, typeDM, sizeDM, special);
                genCritter(9, "C", upp, typeDM, sizeDM, special);
                genCritter(10, "E", upp, typeDM, sizeDM, special);
                genCritter(11, "C", upp, typeDM, sizeDM, special);
                genCritter(12, "C", upp, typeDM, sizeDM, special);
            }
        }

        //---------------------------------------------------------------------------//
        // generate a single critter, by type
        //---------------------------------------------------------------------------//
        protected void genCritter(int dnum, string ctype, UPP upp, int typeDM, int sizeDM, int special)
        {
            // other attributes of a critter
            int size = 0;
            int weapon = 0;
            int armour = 0;
            int attrib = 0; // flyer etc
            int att_roll = roll(6) + roll(6);
            int attack;
            int flee;
            int speed;

            // choose a specific type within the overall class
            int type = roll(6) + roll(6);

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
            if (upp.Size.Value > 8) att_roll -= 1;
            if (upp.Size.Value < 6) att_roll += 1;
            if (upp.Size.Value > 4) att_roll += 1; // cumulative with the above for +2
            if (upp.Atmosphere.Value > 7) att_roll += 1;
            if (upp.Atmosphere.Value < 6) att_roll -= 1;
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
            if (upp.Size.Value > 7) sizeDM -= 1;
            if (upp.Size.Value < 5) sizeDM += 1;


            // derive a size
            size = roll(6) + roll(6) + sizeDM;
            if (size < 1) size = 1;
            if (size > 20) size = 20;
            while (TableData.weights[size][0] == '*')
            {
                size = roll(6) + roll(6) + sizeDM + 6;
                if (size < 1) size = 1;
                if (size > 20) size = 20;
            }

            if (ctype[0] == 'E') size = 0;


            // derive armour
            armour = roll(6) + roll(6);
            if (ctype[0] == 'C') armour -= 1;
            if (ctype[0] == 'S') armour += 1;
            if (ctype[0] == 'H') armour += 2;
            if (armour < 1) armour = 1;
            if (armour > 20) armour = 20;
            while (TableData.armours[armour][0] == '*')
            {
                armour = roll(6) + roll(6) + 6;
                if (ctype[0] == 'C') armour -= 1;
                if (ctype[0] == 'S') armour += 1;
                if (ctype[0] == 'H') armour += 2;
                if (armour < 1) armour = 1;
                if (armour > 20) armour = 20;
            }

            if (ctype[0] == 'E') armour = 0;

            // derive weapons
            weapon = roll(6) + roll(6);
            if (ctype[0] == 'O') weapon += 4;
            if (ctype[0] == 'C') weapon += 8;
            if (ctype[0] == 'H') weapon -= 3;
            if (weapon < 1) weapon = 1;
            if (weapon > 20) weapon = 20;

            if (ctype[0] == 'E') weapon = 0;

            // derive behaviours
            attack = roll(6); flee = roll(6); speed = roll(6); // defaults
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
            prtTable(sw, _tsize, _upp);
        }

        public void WriteToXML(System.Xml.XmlNode parent)
        {
            // initialise counter to iterate through the arrays

            cr_count = 0;
            int dice = _tsize;
            UPP upp = _upp;

            xmlRegion(parent, dice, "Clear, Road, Open", upp);
            xmlRegion(parent, dice, "Prairie, Plain, Steppe", upp);
            xmlRegion(parent, dice, "Rough, Hills, Foothills", upp);
            xmlRegion(parent, dice, "Broken, Badlands", upp);
            xmlRegion(parent, dice, "Mountain, Alpine", upp);
            xmlRegion(parent, dice, "Forest, Woods", upp);
            xmlRegion(parent, dice, "Jungle, Rainforest", upp);
            xmlRegion(parent, dice, "River, Stream, Creek", upp);
            xmlRegion(parent, dice, "Swamp, Bog", upp);
            xmlRegion(parent, dice, "Marsh, Wetland", upp);
            xmlRegion(parent, dice, "Desert, Dunes", upp);
            xmlRegion(parent, dice, "Beach, Shore, Sea Edge", upp);
            xmlRegion(parent, dice, "Surface, Ocean, Sea", upp);
            xmlRegion(parent, dice, "Shallows, Ocean, Sea", upp);
            if (upp.Hydro.Value > 0)
            {
                xmlRegion(parent, dice, "Depths, Ocean, Sea", upp);
                xmlRegion(parent, dice, "Bottom, Ocean, Sea", upp);
            }
            xmlRegion(parent, dice, "Sea Cave, Sea Cavern", upp);
            xmlRegion(parent, dice, "Sargasso, Seaweed", upp);
            xmlRegion(parent, dice, "Ruins, Old City", upp);
            xmlRegion(parent, dice, "Cave, Cavern", upp);
            xmlRegion(parent, dice, "Chasm, Crevass. Abyss", upp);
            xmlRegion(parent, dice, "Crater, Hollow", upp);
        }

        private void xmlRegion(System.Xml.XmlNode parent, int dice, string name, UPP _upp)
        {
            System.Xml.XmlElement region = parent.OwnerDocument.CreateElement("Region");
            System.Xml.XmlElement nameEle = parent.OwnerDocument.CreateElement("name");
            nameEle.InnerText = name;
            region.AppendChild(nameEle);

            if (dice == 1)
            {
                xmlCritter(region, 1);
                xmlCritter(region, 2);
                xmlCritter(region, 3);
                xmlCritter(region, 4);
                xmlCritter(region, 5);
                xmlCritter(region, 6);
            }
            else
            {
                xmlCritter(region, 2);
                xmlCritter(region, 3);
                xmlCritter(region, 4);
                xmlCritter(region, 5);
                xmlCritter(region, 6);
                xmlCritter(region, 7);
                xmlCritter(region, 8);
                xmlCritter(region, 9);
                xmlCritter(region, 10);
                xmlCritter(region, 11);
                xmlCritter(region, 12);
            }
            parent.AppendChild(region);
        }

        private void xmlCritter(System.Xml.XmlElement region, int dnum)
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
        protected void prtTable(TextWriter sw, int dice, UPP upp)
        {
            // initialise counter to iterate through the arrays

            cr_count = 0;

            prtRegion(sw, dice, "Clear, Road, Open", upp);
            prtRegion(sw, dice, "Prairie, Plain, Steppe", upp);
            prtRegion(sw, dice, "Rough, Hills, Foothills", upp);
            prtRegion(sw, dice, "Broken, Badlands", upp);
            prtRegion(sw, dice, "Mountain, Alpine", upp);
            prtRegion(sw, dice, "Forest, Woods", upp);
            prtRegion(sw, dice, "Jungle, Rainforest", upp);
            prtRegion(sw, dice, "River, Stream, Creek", upp);
            prtRegion(sw, dice, "Swamp, Bog", upp);
            prtRegion(sw, dice, "Marsh, Wetland", upp);
            prtRegion(sw, dice, "Desert, Dunes", upp);
            prtRegion(sw, dice, "Beach, Shore, Sea Edge", upp);
            prtRegion(sw, dice, "Surface, Ocean, Sea", upp);
            prtRegion(sw, dice, "Shallows, Ocean, Sea", upp);
            if (upp.Hydro.Value > 0)
            {
                prtRegion(sw, dice, "Depths, Ocean, Sea", upp);
                prtRegion(sw, dice, "Bottom, Ocean, Sea", upp);
            }
            prtRegion(sw, dice, "Sea Cave, Sea Cavern", upp);
            prtRegion(sw, dice, "Sargasso, Seaweed", upp);
            prtRegion(sw, dice, "Ruins, Old City", upp);
            prtRegion(sw, dice, "Cave, Cavern", upp);
            prtRegion(sw, dice, "Chasm, Crevass. Abyss", upp);
            prtRegion(sw, dice, "Crater, Hollow", upp);

        }

        //---------------------------------------------------------------------------//
        // output 1- or 2-dice encounter tables for a named region
        //---------------------------------------------------------------------------//
        protected void prtRegion(TextWriter sw, int dice, string name, UPP upp)
        {
            sw.WriteLine();
            sw.WriteLine("{0,65} UPP {1,6}", name, upp.PhysicalUPP());
            sw.WriteLine("Die  Animal              Weight  Hits  Armour   Weapons           Wounds");
            if (dice == 1)
            {
                prtCritter(sw, 1);
                prtCritter(sw, 2);
                prtCritter(sw, 3);
                prtCritter(sw, 4);
                prtCritter(sw, 5);
                prtCritter(sw, 6);
            }
            else
            {
                prtCritter(sw, 2);
                prtCritter(sw, 3);
                prtCritter(sw, 4);
                prtCritter(sw, 5);
                prtCritter(sw, 6);
                prtCritter(sw, 7);
                prtCritter(sw, 8);
                prtCritter(sw, 9);
                prtCritter(sw, 10);
                prtCritter(sw, 11);
                prtCritter(sw, 12);
            }
        }

        //---------------------------------------------------------------------------//
        // output a single critter, by array index
        // the critter to output is indexed by the global int cr_count
        // and this function increments the index ready for the next call
        //---------------------------------------------------------------------------//
        protected void prtCritter(TextWriter sw, int dnum)
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
        int roll(int sides)
        {
            var die = new Dice(sides);

            int dieRoll = die.roll();
            return (dieRoll);
        }

        //---------------------------------------------------------------------------//
        // find similar critters in the arrays and set their family indicators
        //---------------------------------------------------------------------------//
        protected void findFamily(int cr_num)
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

        private Encounters GetEncounters(int tsize, UPP upp)
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
            if (upp.Hydro.Value > 0)
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
