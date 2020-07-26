using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
namespace Helix.Encryption
{
    public class EncryptorExample : MonoBehaviour
    {
        public Text outputText;
        public Text decryptedText;
        public byte[] encryptedData;
        byte[] key;
        byte[] iv;
        private void Start()
        {
            key = Encoding.ASCII.GetBytes("643B2F7AF0B37A4CB082D55C198088C0");
        }
        public void Encrypt(string input)
        {
            outputText.text = Encryptor.Encrypt(input);
            decryptedText.text = Encryptor.Decrypt(outputText.text);
        }
        public void EncryptAES(string input)
        {
            encryptedData = Encryptor.EncryptAes(input, key, out iv);
  
            outputText.text = Encoding.Default.GetString(encryptedData);
            decryptedText.text = Encryptor.DecryptAes(encryptedData, key, iv);
        }
    }
}   