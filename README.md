# Helix.Encryption
Simple and basic random seed based string encryption system for Unity


# How to use
Include the namespace into your script (using Helix.Encryption)

# Functions

Encryptor.Encrypt(string input)

Returns encrypted input string.

Encryptor.Decrypt(string input)

Returns a decrypted string of the input.

# How it works

It uses a global seed for each character in the input which changes randomly when there is an encryption.
Then it encrypts numbers to numeric UTF32 ints and adds a random letter from the alphabet using the seed.
The UTF32 int result then adds the seed.
All the ints and letters are combined and returns the output string.
For decryption it reads the seed,subtracts the seed for each character and parses every number character.
Then converts UTF32 ints to chars which are then combined to form the output decrypted value.
