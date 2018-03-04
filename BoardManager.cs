using System.Collections.Generic;           //allow us to use Lists
using UnityEngine;
using System;
using Random = UnityEngine.Random;          //Tells Random to use the Unity Engine random

namespace Completed
{

    public class BoardManager : MonoBehaviour {

        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }
        //size of game board:8**
        public int columns = 8;
        public int rows = 8;
        public Count wallCount = new Count (5,9); //setting walls number btw 5 to 9 per level
        public Count foodCount = new Count (1,5); //setting food number btw 1 to 5 per level
        public GameObject exit;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;
        public GameObject[] enemyTiles;
        public GameObject[] outerWallTiles;

        private Transform boardHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        void InitialiseList()
        {
            //clear list gridPositions
            gridPositions.Clear();

            //create a list of positions to place objects like walls, enemy
            for (int x = 1; x < columns - 1; x++)
            {
                for (int y = 1; y < rows - 1; y++)
                    //At each index add a new Vector3 to our list with x & y coordinates of that position
                    gridPositions.Add(new Vector3(x, y, 0f));
                
            }
        }

        //setup outerwall and floor of game board
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform
            boardHolder = new GameObject("Board").transform;

            for (int x = -1; x < columns + 1; x++)
            {
                for (int y = -1; y < rows + 1; y++)
                {
                    //choose floor tile prefabs to prapare to instantiate
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //if current position is at board edge, choose outerwall prefabs
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                    // Instantiate the GameObject instance using the chosen prefabs at Vector3
                    GameObject instance = 
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //avoid cluttering hierarchy
                    instance.transform.SetParent(boardHolder);
                    
                }
            }

        }

        //returns a random position in List gridPositions
        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            //remove the entry at randomIndex from the list so that it can't be re-used
            gridPositions.RemoveAt(randomIndex);
            
            //return random selected Vecter3 position
            return randomPosition;
        }

        //setup tiles(items) including foods, soda, walls
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
    
        // instantiate objects until reaching the objectCount limit
            for(int i = 0; i < objectCount; i++)
            {
                Vector3 randomPosition = RandomPosition(); //choose a random position
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];//choose a random tile
                Instantiate(tileChoice, randomPosition, Quaternion.identity); // instantiate the tile we chose at that position
            }

        }

        public void SetupScene(int level)
        {
            //create outerwall and floor
            BoardSetup();

            //reset list of gridpositions
            InitialiseList();
            
            //instantiate a random number of walls randomly
            LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

            //instantiate a random number of foods randomly
            LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

            //enemy number at particular level
            int enemyCount = (int)Mathf.Log(level, 2f); 

            //cuz the enemy number has been chose, no min and max here
            LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount); 

            //instantiate exit at (7,7)
            Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity); //exit will always be at (7,7)


        }
    }
}