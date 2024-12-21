using FileOperations.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOperations.Commands
{
    public static class FileCommand
    {
        public static bool Create(string path, string fileName)
        {
            try
            {
                string filePath = Path.Combine(path, fileName);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Dispose();
                    Message.WriteSuccess($"File has been created. Path: {filePath}.");
                    return true;
                }
                else
                {
                    Message.WriteError($"File '{filePath}' already exists.");
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while creating the file: {ex.Message}.");
            }
            return false;
        }

        public static bool Delete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Message.WriteSuccess($"File '{filePath}' has been deleted.");
                    return true;
                }
                else
                {
                    Message.WriteError($"File '{filePath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while deleting the file: {ex.Message}.");
            }
            return false;
        }

        public static bool Write(string filePath, string content, bool append = false)
        {
            try
            {
                if (append)
                {
                    File.AppendAllText(filePath, content + Environment.NewLine);
                    Message.WriteSuccess($"Content appended to '{filePath}' successfully.");
                    return true;
                }
                else
                {
                    File.WriteAllText(filePath, content);
                    Message.WriteSuccess($"Content written to '{filePath}' successfully.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while writing to the file: {ex.Message}.");
            }
            return false;
        }
        public static bool Read(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    Message.WriteSuccess($"Content of '{filePath}':\n{content}");
                    return true;
                }
                else
                {
                    Message.WriteError($"File '{filePath}' does not exist.");
                }
            }
            catch (Exception ex)
            {
                Message.WriteError($"An error occurred while reading the file: {ex.Message}.");
            }
            return false;
        }

    }
}
