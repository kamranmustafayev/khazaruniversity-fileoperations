using FileOperations.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperations.Commands
{
    public static class DirectoryCommand
    {
        public static bool Create(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    Console.WriteLine($"Directory '{directoryPath}' created successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Directory '{directoryPath}' already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while creating the directory: {ex.Message}");
            }
            return false;
        }

        public static bool Delete(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true);
                    Console.WriteLine($"Directory '{directoryPath}' deleted successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Directory '{directoryPath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting the directory: {ex.Message}");
            }
            return false;
        }

        public static bool List(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Console.WriteLine($"Contents of directory '{directoryPath}':\n");

                    string[] subdirectories = Directory.GetDirectories(directoryPath);
                    Console.WriteLine("Subdirectories:");
                    foreach (string subdirectory in subdirectories)
                    {
                        Console.WriteLine($"  {subdirectory}");
                    }

                    string[] files = Directory.GetFiles(directoryPath);
                    Console.WriteLine("\nFiles:");
                    foreach (string file in files)
                    {
                        Console.WriteLine($"  {file}");
                    }
                    return true;
                }
                else
                {
                    Message.WriteError($"Directory '{directoryPath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while listing the directory: {ex.Message}.");
            }
            return false;
        }

        public static string GetParent(string currentDirectory)
        {
            try
            {
                string parentDirectory = Directory.GetParent(currentDirectory)?.FullName;

                if (parentDirectory != null)
                {
                    return parentDirectory;
                }
                else
                {
                    Message.WriteError("The current directory has no parent (e.g., root directory).");
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while getting the parent directory: {ex.Message}.");
            }
            return string.Empty;
        }

        public static string GetDirectory(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    return directoryPath;
                }
                else
                {
                    Message.WriteError($"Directory '{directoryPath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while performing operation: {ex.Message}.");
            }
            return string.Empty;
        }
    }
}
