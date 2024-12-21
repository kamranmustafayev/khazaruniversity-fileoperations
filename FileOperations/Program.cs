using FileOperations.Commands;
using FileOperations.Helpers;
using System.IO;

namespace FileOperations
{
    internal class Program
    {
        private readonly static string _programPath = "C:\\Users\\LENOVO\\Desktop\\FileOperations";
        private static string currentPath = _programPath;
        static void Main(string[] args)
        {
            bool isRunning = true;
            Message.WriteInfo("Type 'help' to show commands.");
            while (isRunning)
            {
                string? console = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(console)) Message.WriteInfo("Type 'help' to show commands.");
                else
                {
                    switch (console)
                    {
                        case "create-file": CreateFile(); break;
                        case "delete-file": DeleteFile(); break;
                        case "read-file": ReadFile(); break;
                        case "write-file": WriteFile(); break;
                        case "create-directory": CreateDirectory(); break;
                        case "delete-directory": DeleteDirectory(); break;
                        case "list-directory": ListDirectory(); break;
                        case "goto": GoTo(); break;
                        case "help": ShowCommands(); break;
                        case "exit": isRunning = false; break;
                        default: Message.WriteError("Incorrect command. Type 'help' to show commands."); break;
                    }
                }
            }
        }

        public static void ShowCommands()
        {
            Message.WriteInfo("File commands: create-file, delete-file, read-file, write-file.");
            Message.WriteInfo("Directory commands: create-directory, delete-directory, list-directory, goto");
        }

        public static void CreateFile()
        {
            Message.WriteInfo("Type the name of the file.");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? fileName = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(fileName)) Message.WriteError("Incorrect file name");
                else
                {
                    isCorrect = FileCommand.Create(currentPath, fileName);
                }
            }
        }

        public static void DeleteFile()
        {
            Message.WriteInfo("Type the name of the file.");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? fileName = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(fileName)) Message.WriteError("Incorrect file name");
                else
                {
                    isCorrect = FileCommand.Delete(Path.Combine(currentPath, fileName));
                }
            }
        }

        public static void WriteFile()
        {
            Message.WriteInfo("Type the name of the file.");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? fileName = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(fileName)) Message.WriteError("Incorrect file name");
                else
                {
                    Console.Write("Content: ");
                    string? content = Console.ReadLine();
                    if (String.IsNullOrEmpty(content)) content = String.Empty;
                    Message.WriteInfo("Type 'true' - to append, 'false' - to write from start");
                    bool isSuccess = false;
                    bool isAppend = false;
                    while (!isSuccess)
                    {
                        isSuccess = Boolean.TryParse(Message.WaitResponse(currentPath), out isAppend);
                        if (!isSuccess) Message.WriteError("Incorrect option.");
                    }

                    isCorrect = FileCommand.Write(Path.Combine(currentPath, fileName), content, isAppend);
                }
            }
        }

        public static void ReadFile()
        {
            Message.WriteInfo("Type the name of the file.");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? fileName = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(fileName)) Message.WriteError("Incorrect file name");
                else
                {
                    isCorrect = FileCommand.Read(Path.Combine(currentPath, fileName));
                }
            }
        }

        public static void CreateDirectory()
        {
            Message.WriteInfo("Type the name of the directory.");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? directoryName = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(directoryName)) Message.WriteError("Incorrect directory name");
                else
                {
                    isCorrect = DirectoryCommand.Create(Path.Combine(currentPath, directoryName));
                }
            }
        }
        
        public static void DeleteDirectory()
        {
            Message.WriteInfo("Type the name of the directory.");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? directoryName = Message.WaitResponse(currentPath);
                if (String.IsNullOrEmpty(directoryName)) Message.WriteError("Incorrect directory name");
                else
                {
                    isCorrect = DirectoryCommand.Delete(Path.Combine(currentPath, directoryName));
                }
            }
        }

        public static void ListDirectory()
        {
            DirectoryCommand.List(currentPath);
        }

        public static void GoTo()
        {
            Message.WriteInfo("Type the name of the directory. Type '.' or empty to go to the parent directory");
            bool isCorrect = false;
            while (!isCorrect)
            {
                string? directoryName = Message.WaitResponse(currentPath);
                if ((String.IsNullOrEmpty(directoryName) || directoryName == "."))
                {
                    if (Path.Equals(currentPath, _programPath)) Message.WriteError("You are currently in the main directory");
                    else
                    {
                        string result = DirectoryCommand.GetParent(currentPath);
                        isCorrect = !String.IsNullOrEmpty(result);
                        if (isCorrect) currentPath = result;
                    }
                }
                else
                {
                    string result = DirectoryCommand.GetDirectory(Path.Combine(currentPath, directoryName));
                    isCorrect = !String.IsNullOrEmpty(result);
                    if (isCorrect) currentPath = result;
                }
            }
        }
    }
}
