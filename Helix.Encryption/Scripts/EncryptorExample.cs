using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Helix.Encryption
{
    public class EncryptorExample : MonoBehaviour
    {
        public Text outputText;
        public Text decryptedText;
        public void Encrypt(string input)
        {
            outputText.text = Encryptor.Encrypt(input);
            decryptedText.text = Encryptor.Decrypt(outputText.text);
        }
    }
}   