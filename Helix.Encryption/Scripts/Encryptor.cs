using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Text;
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
        public static byte[] EncryptAes(string data, byte[] key, out byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Mode = CipherMode.CBC; // Should ajust depending on what you want to encrypt
                aes.Key = key;
                aes.GenerateIV(); // Ensure we use a new IV for each encryption

                using (var cryptoTransform = aes.CreateEncryptor())
                {
                    iv = aes.IV;
                    return cryptoTransform.TransformFinalBlock(Encoding.ASCII.GetBytes(data), 0, data.Length);
                }
            }
        }

        public static string DecryptAes(byte[] data, byte[] key, byte[] iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC; // same as for encryption

                using (var cryptoTransform = aes.CreateDecryptor())
                {
                    return Encoding.Default.GetString(cryptoTransform.TransformFinalBlock(data, 0, data.Length));
                }
            }
        }


    }
}