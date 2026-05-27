using System;

namespace CyberSecurityChatbot_PART_2
{
    // This class controls the chatbot's behaviour.
    // It processes user input and returns responses.
    public class ChatBot
    {
        
        private KeywordResponder _keywords;
        private SentimentDetector _sentiment;
        private MemoryStore _memory;

        // Checks if the chatbot is still waiting
        // for the user to enter their name.
        private bool _awaitingName = true;

        // Stores the previous topic discussed
        // so the chatbot can answer follow-up questions.
        private string _lastTopic = "";

        // Constructor
        // This runs automatically when the chatbot starts.
        public ChatBot()
        {
            // Creates new objects for each feature
            _keywords = new KeywordResponder();
            _sentiment = new SentimentDetector();
            _memory = new MemoryStore();
        }

        // This is the chatbot's first message
        // shown when the application opens.
        public string GetGreeting()
        {
            return "Please enter your name. ";
        }

        // This method processes user input
        // and decides what response the chatbot should give.
        public string ProcessInput(string input)
        {
            // Converts text to lowercase
            // to make keyword matching case insensitive.
            string text = input.ToLower();

            
            // If the chatbot is waiting for a name,
            // it stores the user's name and welcomes them.
            if (_awaitingName)
            {
                _memory.UserName = input;
                _awaitingName = false;

                // Check if the user has used the chatbot before
                if (_memory.IsReturningUser(input))
                {
                    string lastTopic = _memory.GetLastTopic(input);

                    return $"Welcome back my darling {_memory.UserName}! Last time we spoke about {lastTopic}. I am pleased to be in touch with you again! Feel free to ask me anything cybersecurity related such as phishing, scams,password safety and how to stay safe online. Kindly type 'exit' to quit.";
                }

                // First-time user welcome message
                return $"Greetings my darling {_memory.UserName}! I'm Uniqua, your cybersucrity awareness assistant. I am pleased to to be in touch with you and I hope we will work well together! Feel free to ask me anything cybersecurity related such as phishing, scams,password safety and how to stay safe online. Kindly type 'exit' to quit.";
            }

            // EXIT MESSAGE
            // Ends the conversation when the user types exit.
            if (text == "exit")
            {
                return "It was great working with you! Stay safe online and spread awareness about safety online. Goodbye my lovely!";
            }

            
            // Allows the user to ask for more information
            // about the last topic discussed.
            if (text.Contains("tell me more") || text.Contains("explain more"))
            {
                // Gives more information about phishing
                if (_lastTopic == "phishing")
                {
                    return "These messages often create urgency to make victims click malicious links or download harmful attachments. Always verify the sender and avoid suspicious links.";
                }

                // Gives more information about passwords
                if (_lastTopic == "password")
                {
                    return "Your password must be hard to guess and should not include personal information such as birthdays or names.";
                }

                // Gives more information about scams
                if (_lastTopic == "scam")
                {
                    return "Scammers may pretend to be trusted companies or even family members. Always verify before sharing information.";
                }

                // Gives more information about online safety
                if (_lastTopic == "safe")
                {
                    return "Staying safe online also means updating your apps and avoiding public Wi-Fi when sharing sensitive information.";
                }

                // If there is no previous topic saved
                return "Please tell me which topic you would like to know more about.";
            }

            
            // Detects the user's mood/emotion
            // and returns an emotional response.
            Sentiment sentiment = _sentiment.Detect(input);
            string emotionResponse = _sentiment.GetSentimentResponse(sentiment);

            // KEYWORD RECOGNITION + RESPONSES

            // PHISHING OPTION
            // Responds when the user types 1 or mentions phishing.
            if (text == "1" || text.Contains("phishing"))
            {
                // Saves the last topic
                _lastTopic = "phishing";

                // Saves topic to memory
                _memory.StoreTopic("phishing");

                return emotionResponse +
                "Phishing is a cyberattack where criminals pretend to be trusted organizations in emails, texts, phone calls, or fake websites to trick people into revealing sensitive information such as passwords, bank details, or personal data. These messages often create urgency to make victims click malicious links or download harmful attachments. The dangers of phishing include identity theft, financial loss, data breaches, and malware infections. To stay safe, people should avoid clicking suspicious links, verify the sender, check website URLs carefully, and use strong passwords with two-factor authentication.";
            }

            // PASSWORD SAFETY OPTION
            else if (text == "2" || text.Contains("password"))
            {
                // Saves the last topic
                _lastTopic = "password";

                // Saves topic to memory
                _memory.StoreTopic("password");

                return emotionResponse +
                "Passwords are important as they protect accounts, systems, maintain privacy, and personal information from unautorized access. It is essential to ensure that you use strong passwords with a mixture of letters, numbers and special characters. Use upper case here and there. Your password must be hard to guess, An examplle would be #Unpr3d!ct@ble... see what I did there?";
            }

            // ONLINE SCAMS OPTION
            else if (text == "3" || text.Contains("scam"))
            {
                // Saves the last topic
                _lastTopic = "scam";

                // Saves topic to memory
                _memory.StoreTopic("scam");

                return emotionResponse +
                "A scam is a dishonest scheme designed steal valuable things such as money, banking details, identity information. Scammers use fake emails, messages or websites pretending to be trusted organizations to trick people into entering their password or banking details. They also use fake lottery or prize scams whereby they trick victims into thinking they have won a prize, and in order to claim the prize, they have to send their ID number. Be careful of scams asking for money or personal details, or clicking suspecious links on emails.";
            }

            // ONLINE SAFETY OPTION
            else if (text == "4" || text.Contains("safe"))
            {
                // Saves the last topic
                _lastTopic = "safe";

                // Saves topic to memory
                _memory.StoreTopic("safe");

                return emotionResponse +
                "Staying safe online means avoiding sharing senstive information online, using strong passwords, avoiding unknown links or downloads as they may contain viruses or malware that may damage devices or steal information. Please keep in mind to always verify websites and avoid clicking suspicious links.";
            }

            // MALWARE
            else if (text.Contains("malware"))
            {
                _lastTopic = "malware";
                _memory.StoreTopic("malware");

                return emotionResponse +
                "Malware is harmful software designed to damage devices, steal information, or spy on users. Examples include viruses and ransomware. To stay safe, avoid suspicious downloads and keep your device updated.";
            }

            // VIRUS
            else if (text.Contains("virus"))
            {
                _lastTopic = "virus";
                _memory.StoreTopic("virus");

                return emotionResponse +
                "A computer virus is a harmful program that spreads between devices and can damage files or slow down performance. Avoid downloading files from unknown websites and always be careful when opening attachments.";
            }

            // RANSOMWARE
            else if (text.Contains("ransomware"))
            {
                _lastTopic = "ransomware";
                _memory.StoreTopic("ransomware");

                return emotionResponse +
                "Ransomware is a type of malware that locks files or devices until money is paid. Avoid suspicious email attachments and regularly back up important files to stay protected.";
            }

            // HACKING
            else if (text.Contains("hacker") || text.Contains("hacking"))
            {
                _lastTopic = "hacking";
                _memory.StoreTopic("hacking");

                return emotionResponse +
                "Hackers are people who try to gain unauthorised access to systems or information. Some hackers help improve security, while others may try to steal information or cause harm.";
            }

            else if (text.Contains("social engineering"))
            {
                _lastTopic = "social engineering";
                _memory.StoreTopic("social engineering");

                return emotionResponse +
                "Social engineering is when attackers trick people into giving away personal information by pretending to be trustworthy. It often relies on manipulation rather than hacking systems directly.";
            }

            // CYBERBULLYING
            else if (text.Contains("cyberbullying") || text.Contains("bullying"))
            {
                _lastTopic = "cyberbullying";
                _memory.StoreTopic("cyberbullying");

                return emotionResponse +
                "Cyberbullying is when someone uses technology like messages, social media or emails to harm, harass, or embarrass another person. Always report and block harmful behaviour.";
            }

            // EMAIL SAFETY
            else if (text.Contains("email") || text.Contains("email safety"))
            {
                _lastTopic = "email";
                _memory.StoreTopic("email");

                return emotionResponse +
                "Email safety involves being careful with suspicious messages, avoiding unknown attachments, and never sharing personal information through untrusted emails.";
            }

            // SUSPICIOUS LINKS
            else if (text.Contains("suspicious link") || text.Contains("unknown link") || text.Contains("click link"))
            {
                _lastTopic = "suspicious links";
                _memory.StoreTopic("suspicious links");

                return emotionResponse +
                "Suspicious links are dangerous because they may lead to fake websites or install malware. Always check the URL before clicking.";
            }

            // PUBLIC WI-FI
            else if (text.Contains("public wifi") || text.Contains("public wi-fi") || text.Contains("wifi safety"))
            {
                _lastTopic = "public wifi";
                _memory.StoreTopic("public wifi");

                return emotionResponse +
                "Public Wi-Fi is not always safe because hackers can intercept your data. Avoid logging into banking or sensitive accounts on public networks.";
            }

            // PRIVACY
            else if (text.Contains("privacy") || text.Contains("data privacy"))
            {
                _lastTopic = "privacy";
                _memory.StoreTopic("privacy");

                return emotionResponse +
                "Data privacy means protecting your personal information online and controlling who can access it.";
            }

            // VPN
            else if (text.Contains("vpn"))
            {
                _lastTopic = "vpn";
                _memory.StoreTopic("vpn");

                return emotionResponse +
                "A VPN (Virtual Private Network) hides your internet activity and encrypts your connection to improve online privacy and security.";
            }

            // GENERAL RESPONSES

            // Greeting response
            else if (text.Contains("hello") || text.Contains("hi"))
            {
                return "Hello! How can I assist you with cybersecurity today?";
            }

            // Responds when user asks how the bot is doing
            else if (text.Contains("how are you"))
            {
                return "As a bot I have no feelings, thanks for asking though. Hope you're doing good. How may I assist you?";
            }

            // Explains chatbot purpose
            else if (text.Contains("purpose"))
            {
                return "My purpose is to help you stay safe online and understand cybersecurity.";
            }

            
            // Displays when the chatbot
            // does not recognise the input.
            return "I didn't understand that. Try asking about phishing, passwords, scams or online safety.";
        }
    }
}