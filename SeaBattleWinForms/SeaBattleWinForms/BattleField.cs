using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SeaBattleWinForms
{
    enum eDirection { up = 0, down, right, left };

    enum eOptionShots { missed = -1, repeatShot = 0, hit = 1, win = 2, destruction = 3 };

    enum eBotShots { missed = 0, hit = 1, destruction = 2 };

    enum eStatus { reserved = -1, empty = 0, shooted = 1, destroyed = 2 };


    class BattleField
    {
        public const int COUNT_OF_SHIP = 10;
        public const int SIZE_X = 10;
        public const int SIZE_Y = 10;




        #region ShipClass

        abstract class BaseShip
        {
            public struct Deck
            {
                public int X { get; set; }
                public int Y { get; set; }

                public bool Alive;

            }

            public Deck[] decks;
            public bool Alive;

            public BaseShip() { Alive = true; }
        }

        class FourDeckShip : BaseShip
        {
            public readonly int SIZE = 4;

            public FourDeckShip()
            {
                decks = new Deck[SIZE];
                for (int i = 0; i < decks.Length; i++) { decks[i].Alive = true; }
            }
        }
        class ThreeDeckShip : BaseShip
        {
            public readonly int SIZE = 3;

            public ThreeDeckShip()
            {
                decks = new Deck[SIZE];
                for (int i = 0; i < decks.Length; i++) { decks[i].Alive = true; }
            }
        }
        class DoubleDeckShip : BaseShip
        {
            public readonly int SIZE = 2;

            public DoubleDeckShip()
            {
                decks = new Deck[SIZE];
                for (int i = 0; i < decks.Length; i++) { decks[i].Alive = true; }
            }
        }
        class SingleDeckShip : BaseShip
        {
            public readonly int SIZE = 1;

            public SingleDeckShip()
            {
                decks = new Deck[SIZE];
                for (int i = 0; i < decks.Length; i++) { decks[i].Alive = true; }
            }
        }
        #endregion

        #region AI
        private struct AI
        {
            public bool currentShip;

            public uint repeatShots;

            public int firstShotX;
            public int firstShotY;

            public int currShotX;
            public int currShotY;

            public bool rightDirection;
            public eDirection direction;

            public List<eDirection> invalidDirection;
        }
        #endregion

        #region ManPlacement

        private class ManPlacement
        {
            private readonly Color DETECTION_COLOR;
            private BaseShip[] ships;
            private bool[,] reserveMatrix;
            List<KeyValuePair<int, int>> tempShip;

            public ManPlacement(Color detectionsСolor)
            {
                DETECTION_COLOR = detectionsСolor;


                ships = new BaseShip[COUNT_OF_SHIP];

                ships[0] = new FourDeckShip();
                ships[1] = new ThreeDeckShip();
                ships[2] = new ThreeDeckShip();
                ships[3] = new DoubleDeckShip();
                ships[4] = new DoubleDeckShip();
                ships[5] = new DoubleDeckShip();
                ships[6] = new SingleDeckShip();
                ships[7] = new SingleDeckShip();
                ships[8] = new SingleDeckShip();
                ships[9] = new SingleDeckShip();


                reserveMatrix = new bool[SIZE_X, SIZE_Y];


                for (int ship = 0; ship < ships.Length; ship++)
                {
                    for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                    {
                        ships[ship].decks[deck].X = -1;
                        ships[ship].decks[deck].Y = -1;
                    }
                }

            }


            public bool TryPlacement(ref Button[,] thisField, ref BaseShip[] otherShips )
            {
                bool check = true;

                int thisDeck = 0;
                int maxDeck = 0;


                for (int x = 0; x < thisField.GetLength(0); x++)
                {
                    for (int y = 0; y < thisField.GetLength(1); y++)
                    {
                        if (thisField[x, y].BackColor == DETECTION_COLOR) { thisDeck++; }
                    }
                }

                for (int i = 0; i < ships.Length; i++)
                {
                    maxDeck += ships[i].decks.Length;
                }


                //Если мах НЕ равняєиса количество кораблей равняєтса текущему
                if (thisDeck != maxDeck) { return false; }


                if (thisDeck == maxDeck)
                {

                    while (check)
                    {
                        check = false;
                        for (int x = 0; x < SIZE_X; x++)
                        {
                            for (int y = 0; y < SIZE_Y; y++)
                            {
                                if (thisField[x, y].BackColor == DETECTION_COLOR)
                                {
                                    //Если кординати есть корблем
                                    if (CheckShipCoordinate(x, y)) { }
                                    else
                                    {
                                        check = true;

                                        //НЕ есть кораблем и зарезервированая
                                        if (reserveMatrix[x, y] == true) { return false; }
                                        else
                                        {
                                            //Добавить и создать корбль
                                            if (AddShip(thisField, x, y)) { }
                                            else
                                            {
                                                return false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }


                //Сделать все корабли живеє
                for (int ship = 0; ship < ships.Length; ship++)
                {
                    ships[ship].Alive = true;
                }


                otherShips = ships;
                return true;
            }


            //Добавить кординати вистрела в наши даниє
            private void AddUsingCoordinate(int x, int y)
            {
                if (!(x < 10 && x >= 0 && y < 10 && y >= 0)) { return; }

                reserveMatrix[x, y] = true;
            }
            private void AddShotCoordinatesShip(int currShip)
            {
                for (int i = 0; i < ships[currShip].decks.Length; i++)
                {
                    // вспомогательние координати
                    AddUsingCoordinate(ships[currShip].decks[i].X, ships[currShip].decks[i].Y);

                    AddUsingCoordinate(ships[currShip].decks[i].X + 1, ships[currShip].decks[i].Y);

                    AddUsingCoordinate(ships[currShip].decks[i].X - 1, ships[currShip].decks[i].Y);

                    AddUsingCoordinate(ships[currShip].decks[i].X, ships[currShip].decks[i].Y + 1);

                    AddUsingCoordinate(ships[currShip].decks[i].X, ships[currShip].decks[i].Y - 1);

                    AddUsingCoordinate(ships[currShip].decks[i].X + 1, ships[currShip].decks[i].Y + 1);

                    AddUsingCoordinate(ships[currShip].decks[i].X + 1, ships[currShip].decks[i].Y - 1);

                    AddUsingCoordinate(ships[currShip].decks[i].X - 1, ships[currShip].decks[i].Y + 1);

                    AddUsingCoordinate(ships[currShip].decks[i].X - 1, ships[currShip].decks[i].Y - 1);
                }
            }
            private bool CheckShipCoordinate(int x, int y)
            {
                for (int ship = 0; ship < ships.Length; ship++)
                {
                    for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                    {
                        if (x == ships[ship].decks[deck].X && y == ships[ship].decks[deck].Y)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
            private bool AddShip(Button[,] thisField, int _x, int _y)
            {
                bool check = true;

                tempShip = new List<KeyValuePair<int, int>>();
                tempShip.Add(new KeyValuePair<int, int>(_x, _y));

                int x = _x;
                int y = _y;



                if (y + 1 < 10 && thisField[x, y + 1].BackColor == DETECTION_COLOR)
                {
                    while (check)
                    {
                        check = false;
                        if (y + 1 < 10 && thisField[x, y + 1].BackColor == DETECTION_COLOR)
                        {
                            y++;

                            tempShip.Add(new KeyValuePair<int, int>(x, y));
                            check = true;
                        }
                    }

                }
                else if (x + 1 < 10 && thisField[x + 1, y].BackColor == DETECTION_COLOR)
                {
                    while (check)
                    {
                        check = false;
                        if (x + 1 < 10 && thisField[x + 1, y].BackColor == Color.Red)
                        {
                            x++;

                            tempShip.Add(new KeyValuePair<int, int>(x, y));
                            check = true;
                        }
                    }
                }




                for (int ship = 0; ship < ships.Length; ship++)
                {
                    if (ships[ship].decks.Length == tempShip.Count && ships[ship].Alive == true)
                    {
                        AddShotCoordinatesShip(ship);
                        ships[ship].Alive = false;


                        for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                        {
                            ships[ship].decks[deck].X = tempShip[deck].Key;
                            ships[ship].decks[deck].Y = tempShip[deck].Value;
                        }

                        return true;
                    }
                }

                return false;
            }
        }

        #endregion


        //Public information
        public bool Win { get; private set; }
        public int countOfMoves { get; private set; }


        //Logic AI and Man Placment
        private AI aI = new AI();
        private ManPlacement manPlacement;


        //information about the coordinates of the shots
        private bool[,] myShot;


        //information about the ALL coordinates
        private int[,] myField;
        private int[,] enemyField;
        private bool[,] myShipsField;


        //information about the coordinates of the ship
        private BaseShip[] ships;


        //Bot state
        private int botStates;


        //ctor
        public BattleField()
        {
            ships = new BaseShip[COUNT_OF_SHIP];

            ships[0] = new FourDeckShip();
            ships[1] = new ThreeDeckShip();
            ships[2] = new ThreeDeckShip();
            ships[3] = new DoubleDeckShip();
            ships[4] = new DoubleDeckShip();
            ships[5] = new DoubleDeckShip();
            ships[6] = new SingleDeckShip();
            ships[7] = new SingleDeckShip();
            ships[8] = new SingleDeckShip();
            ships[9] = new SingleDeckShip();



            //AI
            aI.currentShip = false;
            aI.invalidDirection = new List<eDirection>();
            aI.repeatShots = 3000;


            Win = false;
            countOfMoves = 0;

            //initialize array
            myShot = new bool[SIZE_X, SIZE_Y];

            myField = new int[SIZE_X, SIZE_Y];
            enemyField = new int[SIZE_X, SIZE_Y];
            myShipsField = new bool[SIZE_X, SIZE_Y];

            botStates = (int)eBotShots.missed;
        }




        //Bot shoots another bot
        public bool ShotBot(BattleField bot, int freezeTime)
        {

            System.Threading.Thread.Sleep(freezeTime);


            //Проверка на потоплений корабль
            ShipsCheck(bot);

            //Проверка на подбитий корабль
            WreckedShipCheck(bot);

            //Проверка на победу
            CheckForVictory(bot);
            if (Win == true) { return false; }


            if (botStates == (int)eBotShots.missed)
            {
                //Подщет ходов
                countOfMoves++;

                int randomCount = 0;

            //goto
            TryAgain:

                randomCount++;

                //Вибирае точки
                Random rand = new Random();

                int tempX = rand.Next(0, 10);
                int tempY = rand.Next(0, 10);


                // Защита от вечного цикла
                if (randomCount > aI.repeatShots)
                {
                    WreckedShipCheck(bot);
                    if (Win == true) { return false; }
                    for (int x = 0; x < SIZE_X; x++)
                    {
                        for (int y = 0; y < SIZE_Y; y++)
                        {
                            if (CheckNegative(x, y))
                            {
                                tempX = x;
                                tempY = y;
                                randomCount = 0;
                            }
                        }
                    }
                }


                //Проверка на простреляние точки
                if (!PointCheck(tempX, tempY)) { goto TryAgain; }


                // Добавления елементов в постреляние точки
                AddUsingCoordinate(tempX, tempY);


                int wreckedShip = 0;

                // Проверка на попадание
                if (HitCheck(bot, tempX, tempY, ref wreckedShip))
                {
                    aI.firstShotX = tempX;
                    aI.firstShotY = tempY;
                }
                else
                {
                    botStates = (int)eBotShots.missed;
                    return false;
                }


                // Проверка на живой корабль у бота
                IsTheShipAlive(wreckedShip, bot);


                //Если корабль потоплен
                if (bot.ships[wreckedShip].Alive == false)
                {
                    //Очищаем AI
                    aI.currentShip = false;
                    aI.invalidDirection.Clear();

                    // Добавляем простреляние кординати
                    AddShotCoordinatesShip(bot, wreckedShip);


                    botStates = (int)eBotShots.destruction;
                }
                //Если не потоплен
                else
                {
                    aI.currentShip = true;

                    aI.firstShotX = tempX;
                    aI.firstShotY = tempY;

                    aI.currShotX = tempX;
                    aI.currShotY = tempY;


                    botStates = (int)eBotShots.hit;
                }

                return true;
            }
            else if (botStates == (int)eBotShots.destruction)
            {
                int randomCount = 0;

            //goto
            TryAgain:

                randomCount++;

                //Вибирае точки
                Random rand = new Random();

                int tempX = rand.Next(0, 10);
                int tempY = rand.Next(0, 10);


                // Защита от вечного цикла + опит бота
                if (randomCount > aI.repeatShots)
                {
                    WreckedShipCheck(bot);
                    if (Win == true) { return false; }
                    for (int x = 0; x < SIZE_X; x++)
                    {
                        for (int y = 0; y < SIZE_Y; y++)
                        {
                            if (CheckNegative(x, y))
                            {
                                tempX = x;
                                tempY = y;
                                randomCount = 0;
                            }
                        }
                    }
                }


                //Проверка на простреляние точки
                if (!PointCheck(tempX, tempY)) { goto TryAgain; }


                // Добавления елементов в постреляние точки
                AddUsingCoordinate(tempX, tempY);

                int wreckedShip = 0;

                // Проверка на попадание
                if (HitCheck(bot, tempX, tempY, ref wreckedShip))
                {
                    aI.firstShotX = tempX;
                    aI.firstShotY = tempY;
                }
                else
                {
                    botStates = (int)eBotShots.missed;
                    return false;
                }


                // Проверка на живой корабль у бота
                IsTheShipAlive(wreckedShip, bot);


                //Если корабль потоплен
                if (bot.ships[wreckedShip].Alive == false)
                {
                    //Очищаем AI
                    aI.currentShip = false;
                    aI.invalidDirection.Clear();

                    // Добавляем простреляние кординати
                    AddShotCoordinatesShip(bot, wreckedShip);


                    botStates = (int)eBotShots.destruction;
                    return true;

                }
                //Если не потоплен
                else
                {
                    aI.currentShip = true;

                    aI.firstShotX = tempX;
                    aI.firstShotY = tempY;

                    aI.currShotX = tempX;
                    aI.currShotY = tempY;


                    botStates = (int)eBotShots.hit;
                    return true;
                }


            }
            else if (botStates == (int)eBotShots.hit)
            {
                if (aI.rightDirection == true)
                {
                    int tempX = aI.currShotX;
                    int tempY = aI.currShotY;


                    switch (aI.direction)
                    {
                        case eDirection.up:
                            tempY--;
                            break;
                        case eDirection.down:
                            tempY++;
                            break;
                        case eDirection.right:
                            tempX++;
                            break;
                        case eDirection.left:
                            tempX--;
                            break;
                    }


                    // Проверка на вистрел в уже стреляемоє + инвертация координат
                    if (!PointCheck(tempX, tempY))
                    {
                        //Улучения ИИ
                        aI.currShotX = aI.firstShotX;
                        aI.currShotY = aI.firstShotY;

                        switch (aI.direction)
                        {
                            case eDirection.up:
                                aI.direction = eDirection.down;
                                break;
                            case eDirection.down:
                                aI.direction = eDirection.up;
                                break;
                            case eDirection.right:
                                aI.direction = eDirection.left;
                                break;
                            case eDirection.left:
                                aI.direction = eDirection.right;
                                break;
                        }

                        botStates = (int)eBotShots.hit;
                        return true;
                    }


                    // Добавления простелених кординат
                    AddUsingCoordinate(tempX, tempY);


                    // Проверка всех кораблей на жизнепригодность
                    ShipsCheck(bot);


                    // Вистрел
                    int currentShip = 0;
                    if (HitCheck(bot, tempX, tempY, ref currentShip))
                    {
                        // проверка или живой корабль
                        IsTheShipAlive(currentShip, bot);

                        //Если корабль не живой
                        if (bot.ships[currentShip].Alive == false)
                        {
                            aI.currentShip = false;
                            aI.invalidDirection.Clear();

                            AddShotCoordinatesShip(bot, currentShip);

                            aI.rightDirection = false;
                            botStates = (int)eBotShots.destruction;
                            return true;
                        }
                        //Если живой
                        else
                        {
                            aI.rightDirection = true;

                            aI.currShotX = tempX;
                            aI.currShotY = tempY;

                            botStates = (int)eBotShots.hit;
                            return true;
                        }
                    }
                    else
                    {
                        //Улучения ИИ
                        aI.currShotX = aI.firstShotX;
                        aI.currShotY = aI.firstShotY;

                        switch (aI.direction)
                        {
                            case eDirection.up:
                                aI.direction = eDirection.down;
                                break;
                            case eDirection.down:
                                aI.direction = eDirection.up;
                                break;
                            case eDirection.right:
                                aI.direction = eDirection.left;
                                break;
                            case eDirection.left:
                                aI.direction = eDirection.right;
                                break;
                        }

                        botStates = (int)eBotShots.hit;
                        return false;
                    }
                }
                else
                {

                //goto
                NextDir:


                    //Вибор кординат и направления
                    Random random = new Random();

                    int myDir = random.Next(0, 4);

                    int tempX = aI.firstShotX;
                    int tempY = aI.firstShotY;


                    //Установка управления
                    switch (myDir)
                    {
                        case (int)eDirection.up:
                            aI.direction = eDirection.up;
                            break;
                        case (int)eDirection.down:
                            aI.direction = eDirection.down;
                            break;
                        case (int)eDirection.right:
                            aI.direction = eDirection.right;
                            break;
                        case (int)eDirection.left:
                            aI.direction = eDirection.left;
                            break;
                    }


                    // Проверка на направления
                    for (int i = 0; i < aI.invalidDirection.Count; i++)
                    {
                        if (aI.direction == aI.invalidDirection[i]) { goto NextDir; }
                    }


                    switch (aI.direction)
                    {
                        case eDirection.up:
                            tempY--;
                            break;
                        case eDirection.down:
                            tempY++;
                            break;
                        case eDirection.right:
                            tempX++;
                            break;
                        case eDirection.left:
                            tempX--;
                            break;
                    }


                    // Проверка на вистрел в уже стреляемоє + goto
                    if (!PointCheck(tempX, tempY))
                    {
                        aI.invalidDirection.Add(aI.direction);
                        goto NextDir;
                    }


                    // Добавления простелених кординат
                    AddUsingCoordinate(tempX, tempY);


                    // Проверка всех кораблей на жизнепригодность
                    ShipsCheck(bot);


                    // Вистрел
                    int currentShip = 0;

                    //Проверка на попадания
                    if (HitCheck(bot, tempX, tempY, ref currentShip))
                    {
                        // проверка или живой корабль
                        IsTheShipAlive(currentShip, bot);

                        //Если корабль не живой
                        if (bot.ships[currentShip].Alive == false)
                        {
                            aI.currentShip = false;
                            aI.invalidDirection.Clear();

                            AddShotCoordinatesShip(bot, currentShip);

                            aI.rightDirection = false;
                            botStates = (int)eBotShots.destruction;
                            return true;
                        }
                        //Если живой
                        else
                        {
                            aI.rightDirection = true;

                            aI.currShotX = tempX;
                            aI.currShotY = tempY;

                            botStates = (int)eBotShots.hit;
                            return true;
                        }
                    }
                    else
                    {
                        aI.invalidDirection.Add(aI.direction);
                        botStates = (int)eBotShots.hit;
                        return false;
                    }
                }
            }


            //На всякий случай
            botStates = (int)eBotShots.missed;
            return false;
        }

        // MAN shoots check eOptionShots
        public int ShotMan(BattleField bot, int x, int y)
        {
            //Проверка на подбитий корабль
            WreckedShipCheck(bot);

            //Проверка на потоплений корабль
            ShipsCheck(bot);

            //Проверка на победу
            CheckForVictory(bot);
            if (Win == true) { return (int)eOptionShots.win; }


            countOfMoves++;


            //Проверка на простреляние точки
            if (!PointCheck(x, y)) { return (int)eOptionShots.repeatShot; }


            // Добавления елементов в постреляние точки
            AddUsingCoordinate(x, y);


            // Проверка на попадание
            int wreckedShip = 0;
            if (HitCheck(bot, x, y, ref wreckedShip)) { }
            //Непопали
            else { return (int)eOptionShots.missed; }



            // Проверка на живой корабль у бота
            IsTheShipAlive(wreckedShip, bot);


            //Если корабль потоплен
            if (bot.ships[wreckedShip].Alive == false)
            {
                // Добавляем простреляние кординати
                AddShotCoordinatesShip(bot, wreckedShip);



                //Проверка на подбитий корабль
                WreckedShipCheck(bot);

                //Проверка на потоплений корабль
                ShipsCheck(bot);

                //Проверка на победу
                CheckForVictory(bot);
                if (Win == true) { return (int)eOptionShots.win; }



                // Повторяем вистрел
                return (int)eOptionShots.destruction;
            }
            //Если не потоплен
            else { return (int)eOptionShots.hit; }


        }



        //placement of ships on the battle field
        public void AutoPlacement()
        {
            bool[,] reserved = new bool[SIZE_X, SIZE_Y];


            for (int ship = 0; ship < ships.Length; ship++)
            {

            //goto
            Again:

                //Generate x and y and DIRECTION 
                Random rand = new Random();

                int tempX = rand.Next(0, 10);
                int tempY = rand.Next(0, 10);

                int direction = rand.Next(0, 4);


                // Проверка на резервирование поля
                if (CheckReserveCordinate(ref reserved, tempX, tempY) == false) { goto Again; }



                for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                {
                    if (deck == 0)
                    {
                        ships[ship].decks[deck].X = tempX;
                        ships[ship].decks[deck].Y = tempY;
                    }
                    else
                    {
                        switch (direction)
                        {
                            case (int)eDirection.up:
                                tempY--;
                                break;
                            case (int)eDirection.down:
                                tempY++;
                                break;
                            case (int)eDirection.right:
                                tempX++;
                                break;
                            case (int)eDirection.left:
                                tempX--;
                                break;
                        }

                        if (CheckReserveCordinate(ref reserved, tempX, tempY) == false) { goto Again; }
                        else
                        {
                            ships[ship].decks[deck].X = tempX;
                            ships[ship].decks[deck].Y = tempY;
                        }
                    }
                }


                for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                {
                    // корабль
                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X, ships[ship].decks[deck].Y);


                    // вспомогательние координати
                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X + 1, ships[ship].decks[deck].Y);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X - 1, ships[ship].decks[deck].Y);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X, ships[ship].decks[deck].Y + 1);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X, ships[ship].decks[deck].Y - 1);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X + 1, ships[ship].decks[deck].Y + 1);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X + 1, ships[ship].decks[deck].Y - 1);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X - 1, ships[ship].decks[deck].Y + 1);

                    AddReserveCordinate(ref reserved, ships[ship].decks[deck].X - 1, ships[ship].decks[deck].Y - 1);

                }

            }
        }
        public bool HumanPlacement(ref Button[,] thisField , Color detectionsСolor)
        {
            //Human Placement
            manPlacement = new ManPlacement(detectionsСolor);

            return manPlacement.TryPlacement(ref thisField, ref ships);
        }



        //Занести корабли на матрицу 
        private void AddInfoOnMyMatrix(BattleField bot)
        {

            // Живие коробли
            for (int ship = 0; ship < ships.Length; ship++)
            {
                for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                {
                    if (ships[ship].decks[deck].Alive == true)
                    {
                        myField[ships[ship].decks[deck].X, ships[ship].decks[deck].Y] = (int)eStatus.reserved;
                    }
                }
            }


            //Попадания
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (bot.myShot[x, y] == true)
                    {
                        myField[x, y] = (int)eStatus.shooted;
                    }
                }
            }


            // Мертвиє коробли
            for (int ship = 0; ship < ships.Length; ship++)
            {
                for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                {
                    if (ships[ship].decks[deck].Alive == false)
                    {
                        myField[ships[ship].decks[deck].X, ships[ship].decks[deck].Y] = (int)eStatus.destroyed;
                    }
                }
            }

        }
        private void AddInfoOnEnemyMatrix(BattleField bot)
        {
            //Попадания
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    if (myShot[x, y] == true)
                    {
                        enemyField[x, y] = (int)eStatus.shooted;
                    }
                }
            }

            // Вибитие корабли
            for (int ship = 0; ship < bot.ships.Length; ship++)
            {
                for (int deck = 0; deck < bot.ships[ship].decks.Length; deck++)
                {
                    if (bot.ships[ship].decks[deck].Alive == false)
                    {
                        enemyField[bot.ships[ship].decks[deck].X, bot.ships[ship].decks[deck].Y] = (int)eStatus.destroyed;
                    }
                }
            }
        }
        private void AddAliveShipsOnMatrix()
        {
            //Clear matrix
            for (int x = 0; x < SIZE_X; x++)
            {
                for (int y = 0; y < SIZE_Y; y++)
                {
                    myShipsField[x, y] = false;
                }
            }

            // Живие коробли
            for (int ship = 0; ship < ships.Length; ship++)
            {
                for (int deck = 0; deck < ships[ship].decks.Length; deck++)
                {
                    int x = ships[ship].decks[deck].X;
                    int y = ships[ship].decks[deck].Y;

                    myShipsField[x, y] = true;
                }
            }
        }

        //Вернути матрици с кораблями
        public int[,] GetMyField(BattleField bot) { AddInfoOnMyMatrix(bot); return myField; }
        public int[,] GetEnemyField(BattleField bot) { AddInfoOnEnemyMatrix(bot); return enemyField; }
        public bool[,] GetMyShips() { AddAliveShipsOnMatrix(); return myShipsField; }



        //ADD reserve coordinate
        private void AddReserveCordinate(ref bool[,] reserved, int x, int y)
        {
            if (!(x < 10 && x >= 0 && y < 10 && y >= 0)) { return; }

            reserved[x, y] = true;
        }
        //Check reserve coordinate
        private bool CheckReserveCordinate(ref bool[,] reserved, int x, int y)
        {
            if (!(x < 10 && x >= 0 && y < 10 && y >= 0)) { return false; }

            if (reserved[x, y] == true) { return false; }
            else { return true; }
        }




        //Добавить кординати вистрела в наши даниє
        private void AddUsingCoordinate(int x, int y)
        {
            if (!(x < 10 && x >= 0 && y < 10 && y >= 0)) { return; }

            myShot[x, y] = true;
        }

        // Проверка точки на совпадения с даними о стреляемих координатах + виход за придели
        private bool PointCheck(int x, int y)
        {
            if (!(x < 10 && x >= 0 && y < 10 && y >= 0)) { return false; }

            if (myShot[x, y] == true) { return false; }

            return true;
        }

        //Проверка на негативний результат
        private bool CheckNegative(int x, int y)
        {
            if (!(x < 10 && x >= 0 && y < 10 && y >= 0)) { return false; }

            if (myShot[x, y] == false) { return true; }
            else { return false; }
        }

        //Проверка на попадания во вражеский корабль + возвращения номера корабля
        private bool HitCheck(BattleField bot, int x, int y, ref int shipNumber)
        {
            for (int ship = 0; ship < bot.ships.Length; ship++)
            {
                if (bot.ships[ship].Alive == false) { continue; }

                for (int deck = 0; deck < bot.ships[ship].decks.Length; deck++)
                {
                    if (bot.ships[ship].decks[deck].Alive == false) { continue; }

                    // hit
                    if (x == bot.ships[ship].decks[deck].X && y == bot.ships[ship].decks[deck].Y)
                    {
                        bot.ships[ship].decks[deck].Alive = false;

                        shipNumber = ship;
                        return true;
                    }
                }
            }

            return false;
        }

        // Проверка живой ли корабль у бота по номеру
        private void IsTheShipAlive(int currShip, BattleField bot)
        {
            bool resultat = false;

            for (int check = 0; check < bot.ships[currShip].decks.Length; check++)
            {
                resultat = resultat || bot.ships[currShip].decks[check].Alive;
            }
            bot.ships[currShip].Alive = resultat;
        }

        // проверка на подбитий корабль у бота проверка на дурака
        private void WreckedShipCheck(BattleField bot)
        {
            for (int ship = 0; ship < bot.ships.Length; ship++)
            {
                if (bot.ships[ship].Alive == false) { continue; }

                for (int deck = 0; deck < bot.ships[ship].decks.Length; deck++)
                {
                    if (bot.ships[ship].decks[deck].Alive == false) { continue; }

                    if (myShot[bot.ships[ship].decks[deck].X, bot.ships[ship].decks[deck].Y] == true)
                    {
                        bot.ships[ship].decks[deck].Alive = false;
                    }
                }
            }

            CheckForVictory(bot);
        }

        //Проверка на нашу победу 
        private void CheckForVictory(BattleField bot)
        {
            bool finish = false;
            for (int i = 0; i < bot.ships.Length; i++) { finish = finish || bot.ships[i].Alive; }
            Win = !finish;
        }

        // Проверка на живность кораблей у бота
        private void ShipsCheck(BattleField bot)
        {
            for (int ship = 0; ship < bot.ships.Length; ship++)
            {
                bool res = false;
                for (int check = 0; check < bot.ships[ship].decks.Length; check++)
                {
                    res = res || bot.ships[ship].decks[check].Alive;
                }
                bot.ships[ship].Alive = res;
            }
        }

        // Добавить простреляние кординати корабля у бота
        private void AddShotCoordinatesShip(BattleField bot, int currShip)
        {
            for (int i = 0; i < bot.ships[currShip].decks.Length; i++)
            {
                // вспомогательние координати
                AddUsingCoordinate(bot.ships[currShip].decks[i].X, bot.ships[currShip].decks[i].Y);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X + 1, bot.ships[currShip].decks[i].Y);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X - 1, bot.ships[currShip].decks[i].Y);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X, bot.ships[currShip].decks[i].Y + 1);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X, bot.ships[currShip].decks[i].Y - 1);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X + 1, bot.ships[currShip].decks[i].Y + 1);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X + 1, bot.ships[currShip].decks[i].Y - 1);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X - 1, bot.ships[currShip].decks[i].Y + 1);

                AddUsingCoordinate(bot.ships[currShip].decks[i].X - 1, bot.ships[currShip].decks[i].Y - 1);
            }
        }


    }
}

