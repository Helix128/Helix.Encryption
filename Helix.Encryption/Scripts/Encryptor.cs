using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
namespace Helix.Encryption
{
    public class Encryptor : MonoBehaviour
    {

        static string abecedario = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        static int[] seed;
        static bool constantSeed;
      
   
        public static string Encrypt(string input)
        {
          
            if (PlayerPrefs.GetInt("cSeed") == 1)
            {
                constantSeed = true;
            }
            else
            {
                constantSeed = false;
            }
            char[] chars = input.ToCharArray();
            int[] ids = new int[chars.Length];
            seed = new int[ids.Length];
            int cseed = 0;
            if (constantSeed&&PlayerPrefs.GetInt("seed",-1)==-1)
            {
                cseed = Random.Range(0, abecedario.Length);
                PlayerPrefs.SetInt("seed", cseed);
            }
            else if (constantSeed)
            {
                cseed = PlayerPrefs.GetInt("seed");
            }
            for (int i = 0; i < chars.Length; i++)
            {
                if (constantSeed)
                {
                    seed[i] = cseed;
                }
                else
                {
                    seed[i] = Random.Range(0, abecedario.Length);
                }
                ids[i] = char.ConvertToUtf32(chars[i].ToString(), 0)+seed[i];
            }
            
            string output = "";
            for (int i = 0; i < ids.Length; i++)
            {
                output += ids[i].ToString() + abecedario[seed[i]]; ;
            }

            return output;


        }
        public static string Decrypt(string input)
        {
            char[] chars = input.ToCharArray();

            List<int> ids = new List<int>();
            string[] rawOutput = Regex.Split(input, @"\D+");

            for (int i = 0; i < rawOutput.Length; i++)
            {
                int value;
                if (int.TryParse(rawOutput[i], out value))
                {
                    
                    ids.Add(value-seed[i]);

                }
            }

            string output = "";
            for (int i = 0; i < ids.Count; i++)
            {
                output += char.ConvertFromUtf32(ids[i]);
            }

            return output;
        }
    }
}