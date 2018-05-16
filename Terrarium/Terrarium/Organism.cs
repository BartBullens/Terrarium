using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terrarium;

namespace Terrarium
{
    public abstract class Organism
    {
        // constructor
        public Organism(int xposition, int yposition) : this(xposition, yposition, 1)
        {
            // calls other constructor
        }

        public Organism(int xposition, int yposition, int moves)
        {
            this.Position = new Coordinate(xposition, yposition);
            IsWalkable = false;
            Moves = moves;
            LastMove = OrganismMoves.Nothing;
            Counter++;
        }

        // lives of organism
        private int lifeValue;

        public int Life
        {
            get;
            set;
        }

        // belongs to which terrarium

        private Terrarium terrariumValue;

        public Terrarium Terrarium
        {
            get;
            set;
        }

        // coordinate in array of organism
        private Coordinate positionValue;

        public Coordinate Position
        {
            get;
            set;
        }

        // sprite ("character") of organism
        // readonly
        private String spriteValue;

        public String Sprite
        {
            get;
            set;
        }

        // can you walk on this array coordinate
        private Boolean isWalkableValue;

        public Boolean IsWalkable
        {
            get;
            set;
        }

        // number of move per day
        private int movesValue;

        public int Moves
        {
            get;
            set;
        }

        // last performed move
        public OrganismMoves LastMove
        {
            get;
            set;
        }

        // organism ID
        public string ID
        {
            get;
            set;
        }

        private static int counterValue;

        public static int Counter
        {
            get
            {
                return counterValue;
            }
            set
            {
                counterValue = value;
            }
        }

        public virtual void DisplayOrganism()
        {
            Console.Write(Sprite);
        }
    }
}