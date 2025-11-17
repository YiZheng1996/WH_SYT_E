using System.Text;

namespace MainUI.CurrencyHelper
{
    /// <summary>
    /// 测试类文件管理器
    /// </summary>
//    public class TestClassFileManager
//    {
//        private readonly string _testClassPath;
//        private readonly ModelTypeBLL _modelTypeBLL;

//        public TestClassFileManager()
//        {
//            _testClassPath = GetProjectSourceTestPath();
//            _modelTypeBLL = new ModelTypeBLL();
//            EnsureDirectoryExists();
//        }

//        /// <summary>
//        /// 获取项目源码中的Test目录路径
//        /// </summary>
//        private string GetProjectSourceTestPath()
//        {
//            try
//            {
//                // 从当前程序集位置开始查找
//                string currentPath = AppDomain.CurrentDomain.BaseDirectory;
//                DirectoryInfo dir = new(currentPath);

//                // 向上查找，直到找到解决方案目录（包含.sln文件）
//                while (dir != null)
//                {
//                    if (dir.GetFiles("*.sln").Length > 0)
//                    {
//                        // 找到解决方案根目录，定位MainUI项目
//                        string mainUIPath = Path.Combine(dir.FullName, "MainUI", "Procedure", "Test");
//                        if (Directory.Exists(Path.Combine(dir.FullName, "MainUI")))
//                        {
//                            return mainUIPath;
//                        }
//                    }
//                    dir = dir.Parent;
//                }

//                // 相对路径
//                return Path.GetFullPath(Path.Combine(currentPath, "..", "..", "..", "Procedure", "Test"));
//            }
//            catch
//            {
//                return Path.Combine(Directory.GetCurrentDirectory(), "Procedure", "Test");
//            }
//        }

//        /// <summary>
//        /// 检测并创建目录
//        /// </summary>
//        private void EnsureDirectoryExists()
//        {
//            if (!Directory.Exists(_testClassPath))
//            {
//                Directory.CreateDirectory(_testClassPath);
//            }
//        }

//        /// <summary>
//        /// 根据ModelTypeID获取分类文件夹路径
//        /// </summary>
//        /// <param name="modelTypeID">ModelType ID，如果为0则使用根目录</param>
//        /// <returns>分类文件夹路径</returns>
//        private string GetModelTypeDirectoryPath(int modelTypeID)
//        {
//            if (modelTypeID <= 0)
//            {
//                return _testClassPath; // 返回根目录
//            }

//            try
//            {
//                // 获取ModelType信息
//                var modelTypes = _modelTypeBLL.GetAllModelType();
//                var modelType = modelTypes.FirstOrDefault(m => m.ID == modelTypeID);

//                if (modelType == null)
//                {
//                    NlogHelper.Default.Warn($"未找到ID为 {modelTypeID} 的ModelType，使用根目录");
//                    return _testClassPath;
//                }

//                // 清理文件夹名称
//                string folderName = CleanFolderName(modelType.ModelTypeName);
//                string modelTypePath = Path.Combine(_testClassPath, folderName);

//                // 确保目录存在
//                if (!Directory.Exists(modelTypePath))
//                {
//                    Directory.CreateDirectory(modelTypePath);
//                    NlogHelper.Default.Info($"创建ModelType分类目录：{modelTypePath}");
//                }

//                return modelTypePath;
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"获取ModelType目录失败：{ex.Message}", ex);
//                return _testClassPath; // 返回根目录作为后备
//            }
//        }

//        /// <summary>
//        /// 根据EntityClassName创建测试类文件
//        /// </summary>
//        /// <param name="entityClassName">实体类名</param>
//        /// <param name="modelTypeID">ModelType ID，用于确定分类文件夹</param>
//        /// <returns>是否创建成功</returns>
//        public bool CreateTestClassByEntityClassName(string entityClassName, int modelTypeID = 0, string processName = "")
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(entityClassName))
//                    return false;

//                // 获取目标目录
//                string targetDirectory = GetModelTypeDirectoryPath(modelTypeID);
//                string fileName = $"{entityClassName}.cs";
//                string filePath = Path.Combine(targetDirectory, fileName);

//                if (File.Exists(filePath))
//                {
//                    NlogHelper.Default.Info($"测试类文件已存在：{fileName}");
//                    return true;
//                }

//                string classContent = GenerateTestClassContent(entityClassName, processName);
//                File.WriteAllText(filePath, classContent, Encoding.UTF8);

//                NlogHelper.Default.Info($"成功创建测试类文件：{filePath}");
//                return true;
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"创建测试类文件失败：{entityClassName}", ex);
//                return false;
//            }
//        }

//        /// <summary>
//        /// 根据EntityClassName删除测试类文件
//        /// </summary>
//        /// <param name="entityClassName">实体类名</param>
//        /// <param name="modelTypeID">ModelType ID，如果为0则在所有目录中查找</param>
//        /// <returns>是否删除成功</returns>
//        public bool DeleteTestClassByEntityClassName(string entityClassName, int modelTypeID = 0)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(entityClassName))
//                    return false;

//                string fileName = $"{entityClassName}.cs";

//                if (modelTypeID > 0)
//                {
//                    // 在指定ModelType目录中删除
//                    string targetDirectory = GetModelTypeDirectoryPath(modelTypeID);
//                    string filePath = Path.Combine(targetDirectory, fileName);

//                    if (File.Exists(filePath))
//                    {
//                        File.Delete(filePath);
//                        NlogHelper.Default.Info($"成功删除测试类文件：{filePath}");
//                    }
//                }
//                else
//                {
//                    // 在所有目录中查找并删除
//                    DeleteFileInAllDirectories(fileName);
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"删除测试类文件失败：{entityClassName}", ex);
//                return false;
//            }
//        }

//        /// <summary>
//        /// 在所有目录中查找并删除文件
//        /// </summary>
//        /// <param name="fileName">文件名</param>
//        private void DeleteFileInAllDirectories(string fileName)
//        {
//            // 在根目录中查找
//            string rootFilePath = Path.Combine(_testClassPath, fileName);
//            if (File.Exists(rootFilePath))
//            {
//                File.Delete(rootFilePath);
//                NlogHelper.Default.Info($"成功删除测试类文件：{rootFilePath}");
//            }

//            // 在所有子目录中查找
//            if (Directory.Exists(_testClassPath))
//            {
//                var subDirectories = Directory.GetDirectories(_testClassPath);
//                foreach (string subDir in subDirectories)
//                {
//                    string filePath = Path.Combine(subDir, fileName);
//                    if (File.Exists(filePath))
//                    {
//                        File.Delete(filePath);
//                        NlogHelper.Default.Info($"成功删除测试类文件：{filePath}");
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 根据EntityClassName重命名测试类文件
//        /// </summary>
//        /// <param name="oldEntityClassName">旧实体类名</param>
//        /// <param name="newEntityClassName">新实体类名</param>
//        /// <param name="modelTypeID">ModelType ID，如果为0则在所有目录中查找</param>
//        /// <returns>是否重命名成功</returns>
//        public bool RenameTestClassByEntityClassName(string oldEntityClassName, string newEntityClassName, int modelTypeID = 0)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(oldEntityClassName) || string.IsNullOrWhiteSpace(newEntityClassName))
//                    return false;

//                if (oldEntityClassName == newEntityClassName)
//                    return true;

//                if (modelTypeID > 0)
//                {
//                    // 在指定ModelType目录中重命名
//                    string targetDirectory = GetModelTypeDirectoryPath(modelTypeID);
//                    return RenameFileInDirectory(oldEntityClassName, newEntityClassName, targetDirectory);
//                }
//                else
//                {
//                    // 在所有目录中查找并重命名
//                    return RenameFileInAllDirectories(oldEntityClassName, newEntityClassName);
//                }
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"重命名测试类文件失败：{oldEntityClassName} -> {newEntityClassName}", ex);
//                return false;
//            }
//        }

//        /// <summary>
//        /// 在指定目录中重命名文件
//        /// </summary>
//        /// <param name="oldClassName">旧类名</param>
//        /// <param name="newClassName">新类名</param>
//        /// <param name="directory">目录路径</param>
//        /// <returns>是否成功</returns>
//        private bool RenameFileInDirectory(string oldClassName, string newClassName,
//            string directory, string processName = "")
//        {
//            try
//            {
//                string oldFilePath = Path.Combine(directory, $"{oldClassName}.cs");
//                string newFilePath = Path.Combine(directory, $"{newClassName}.cs");

//                if (File.Exists(oldFilePath))
//                {
//                    // 读取文件内容并替换类名
//                    string content = File.ReadAllText(oldFilePath, Encoding.UTF8);
//                    content = content.Replace($"class {oldClassName}", $"class {newClassName}");

//                    // 写入新文件并删除旧文件
//                    File.WriteAllText(newFilePath, content, Encoding.UTF8);
//                    File.Delete(oldFilePath);

//                    NlogHelper.Default.Info($"成功重命名测试类文件：{oldFilePath} -> {newFilePath}");
//                    return true;
//                }
//                else
//                {
//                    // 文件不存在，创建新文件
//                    string newContent = GenerateTestClassContent(newClassName, processName);
//                    File.WriteAllText(newFilePath, newContent, Encoding.UTF8);
//                    NlogHelper.Default.Info($"文件不存在，创建新文件：{newFilePath}");
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"在目录 {directory} 中重命名文件失败：{oldClassName} -> {newClassName}", ex);
//                return false;
//            }
//        }

//        /// <summary>
//        /// 在所有目录中查找并重命名文件
//        /// </summary>
//        /// <param name="oldClassName">旧类名</param>
//        /// <param name="newClassName">新类名</param>
//        /// <returns>是否成功</returns>
//        private bool RenameFileInAllDirectories(string oldClassName, string newClassName)
//        {
//            bool anySuccess = false;

//            try
//            {
//                // 在根目录中查找
//                anySuccess |= RenameFileInDirectory(oldClassName, newClassName, _testClassPath);

//                // 在所有子目录中查找
//                if (Directory.Exists(_testClassPath))
//                {
//                    var subDirectories = Directory.GetDirectories(_testClassPath);
//                    foreach (string subDir in subDirectories)
//                    {
//                        anySuccess |= RenameFileInDirectory(oldClassName, newClassName, subDir);
//                    }
//                }

//                // 如果没有找到任何文件，创建新文件
//                if (!anySuccess)
//                {
//                    CreateTestClassByEntityClassName(newClassName);
//                    anySuccess = true;
//                }

//                return anySuccess;
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"在所有目录中重命名文件失败：{oldClassName} -> {newClassName}", ex);
//                return false;
//            }
//        }

//        /// <summary>
//        /// 移动测试类文件到新的ModelType目录
//        /// </summary>
//        /// <param name="entityClassName">实体类名</param>
//        /// <param name="oldModelTypeID">旧ModelType ID</param>
//        /// <param name="newModelTypeID">新ModelType ID</param>
//        /// <returns>是否移动成功</returns>
//        public bool MoveTestClassToNewModelType(string entityClassName, int oldModelTypeID, int newModelTypeID)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(entityClassName) || oldModelTypeID == newModelTypeID)
//                    return true;

//                string fileName = $"{entityClassName}.cs";
//                string oldDirectory = GetModelTypeDirectoryPath(oldModelTypeID);
//                string newDirectory = GetModelTypeDirectoryPath(newModelTypeID);

//                string oldFilePath = Path.Combine(oldDirectory, fileName);
//                string newFilePath = Path.Combine(newDirectory, fileName);

//                if (File.Exists(oldFilePath))
//                {
//                    // 如果目标文件已存在，先备份
//                    if (File.Exists(newFilePath))
//                    {
//                        string backupPath = Path.Combine(newDirectory, $"{entityClassName}_backup_{DateTime.Now:yyyyMMddHHmmss}.cs");
//                        File.Move(newFilePath, backupPath);
//                        NlogHelper.Default.Info($"备份已存在的文件：{newFilePath} -> {backupPath}");
//                    }

//                    // 移动文件
//                    File.Move(oldFilePath, newFilePath);
//                    NlogHelper.Default.Info($"成功移动测试类文件：{oldFilePath} -> {newFilePath}");
//                    return true;
//                }
//                else
//                {
//                    // 源文件不存在，在新目录创建文件
//                    return CreateTestClassByEntityClassName(entityClassName, newModelTypeID);
//                }
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"移动测试类文件失败：{entityClassName}", ex);
//                return false;
//            }
//        }

//        /// <summary>
//        /// 清理文件夹名称中的特殊字符
//        /// </summary>
//        /// <param name="folderName">原文件夹名</param>
//        /// <returns>清理后的文件夹名</returns>
//        private static string CleanFolderName(string folderName)
//        {
//            if (string.IsNullOrWhiteSpace(folderName))
//                return "Unknown";

//            // 移除或替换文件夹名中的特殊字符
//            char[] invalidChars = Path.GetInvalidFileNameChars();
//            string cleanName = folderName;

//            foreach (char invalidChar in invalidChars)
//            {
//                cleanName = cleanName.Replace(invalidChar, '_');
//            }

//            // 替换一些常见的特殊字符
//            cleanName = cleanName.Replace(":", "_")
//                                 .Replace("/", "_")
//                                 .Replace("\\", "_")
//                                 .Replace("*", "_")
//                                 .Replace("?", "_")
//                                 .Replace("\"", "_")
//                                 .Replace("<", "_")
//                                 .Replace(">", "_")
//                                 .Replace("|", "_")
//                                 .Trim();

//            return string.IsNullOrEmpty(cleanName) ? "Unknown" : cleanName;
//        }

//        /// <summary>
//        /// 实体类模板
//        /// </summary>
//        /// <param name="entityClassName"></param>
//        /// <returns></returns>
//        private static string GenerateTestClassContent(string entityClassName, string processName = "")
//        {
//            return $@"namespace MainUI.Procedure.Test
//{{
//    /// <summary>
//    /// {processName}
//    /// </summary>
//    public class {entityClassName} : GeneralBaseTest
//    {{
//        public override async Task<bool> Execute(CancellationToken cancellationToken)
//        {{
//            await base.Execute(cancellationToken);
//            try
//            {{
//                // 使用示例
//                // 简单延时 2 秒
//                Delay(2.0);

//                // 等待条件满足（最多10秒）
//                // wait() 返回 true 表示条件满足，退出等待
//                bool timeout = Delay(10.0, 100, () => OPCHelper.AIgrp[0] > 100.0);
//                if (timeout)
//                {{
//                    TxtTips(""等待超时"");
//                    return false;
//                }}

//                if (!ShowConfirmDialog(""是否继续?""))
//                {{
//                    return false;
//                }}

//                // 带图标类型的确认
//                if (!ShowConfirmDialog(""检测到异常，是否继续?"", TType.Warn))
//                {{
//                    return false;
//                }}

//                // 显示各种提示
//                ShowSuccessDialog(""操作成功"");
//                ShowWarningDialog(""注意检查"");
//                ShowErrorDialog(""操作失败"");
//                ShowInfoDialog(""提示信息"");

//                //  等待多个条件之一满足（最多5秒）
//                Delay(5, 100,
//                    () => OPCHelper.DIgrp[1],           // 条件1：DI[1] 为 true
//                    () => OPCHelper.AIgrp[0] > 50.0     // 条件2：AI[0] > 50
//                );

//                //  带步骤名称的延时（显示倒计时）
//                Delay(30, 100, () => false, ""预热阶段"");

//                return true;
//            }}
//            catch (Exception ex)
//            {{
//                NlogHelper.Default.Error($""{entityClassName}：{{ex.Message}}"");
//                throw new($""{entityClassName}：{{ex.Message}}"");
//            }}
//            finally
//            {{
//                // 试验结束后的清理操作
//            }}
//        }}
//    }}
//}}
//";
//        }

//        /// <summary>
//        /// 获取指定ModelType的所有测试类文件
//        /// </summary>
//        /// <param name="modelTypeID">ModelType ID</param>
//        /// <returns>测试类文件名列表（不含扩展名）</returns>
//        public List<string> GetTestClassFilesByModelType(int modelTypeID)
//        {
//            try
//            {
//                string targetDirectory = GetModelTypeDirectoryPath(modelTypeID);

//                if (!Directory.Exists(targetDirectory))
//                    return new List<string>();

//                return Directory.GetFiles(targetDirectory, "*.cs")
//                                .Select(Path.GetFileNameWithoutExtension)
//                                .ToList();
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error($"获取ModelType {modelTypeID} 的测试类文件失败", ex);
//                return new List<string>();
//            }
//        }

//        /// <summary>
//        /// 清理空的ModelType目录
//        /// </summary>
//        public void CleanupEmptyModelTypeDirectories()
//        {
//            try
//            {
//                if (!Directory.Exists(_testClassPath))
//                    return;

//                var subDirectories = Directory.GetDirectories(_testClassPath);
//                foreach (string subDir in subDirectories)
//                {
//                    // 检查目录是否为空（没有.cs文件）
//                    if (!Directory.GetFiles(subDir, "*.cs").Any())
//                    {
//                        Directory.Delete(subDir, true);
//                        NlogHelper.Default.Info($"清理空的ModelType目录：{subDir}");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error("清理空ModelType目录失败", ex);
//            }
//        }

//        /// <summary>
//        /// 获取所有ModelType目录的统计信息
//        /// </summary>
//        /// <returns>目录统计信息</returns>
//        public Dictionary<string, int> GetModelTypeDirectoryStats()
//        {
//            var stats = new Dictionary<string, int>();

//            try
//            {
//                if (!Directory.Exists(_testClassPath))
//                    return stats;

//                // 根目录统计
//                var rootFiles = Directory.GetFiles(_testClassPath, "*.cs");
//                if (rootFiles.Length > 0)
//                {
//                    stats["根目录"] = rootFiles.Length;
//                }

//                // 子目录统计
//                var subDirectories = Directory.GetDirectories(_testClassPath);
//                foreach (string subDir in subDirectories)
//                {
//                    var files = Directory.GetFiles(subDir, "*.cs");
//                    if (files.Length > 0)
//                    {
//                        string dirName = Path.GetFileName(subDir);
//                        stats[dirName] = files.Length;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                NlogHelper.Default.Error("获取ModelType目录统计信息失败", ex);
//            }

//            return stats;
//        }
//    }
}