using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


    public class Utils
    {
        public static Color SwitchColor(int degree)
        {
            switch (degree)
            {
                case 0:
                    return Color.red;
                case 1:
                    return Color.green;
                 
                case 2:
                    return Color.yellow;
              
                case 3:
                    return  Color.blue;
                 
                case 4:
                    return Color.gray;
                  
                case 5:
                    return Color.grey;
               
                case 6:
                    return Color.cyan;
                
                case 7:
                    return Color.black;
                default:
                    return Color.white;
            }
        }
    }

