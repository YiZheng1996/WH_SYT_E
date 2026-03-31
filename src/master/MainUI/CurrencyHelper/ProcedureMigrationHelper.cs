using MainUI.Service;

/// <summary>
/// 将旧路径 Procedure/{类型}/{型号名}/ 迁移到新路径 Procedure/{类型}/{ID}_{型号名}/
/// 只需执行一次
/// </summary>
public static class ProcedureMigrationHelper
{
    public static void MigrateOldFolders()
    {
        try
        {
            string procedureRoot = Path.Combine(Application.StartupPath, "Procedure");
            if (!Directory.Exists(procedureRoot)) return;

            // 查询所有型号，建立 ModelName → NewModels 的映射
            var allModels = ModelBLL.GetAllNewModels(); // 需要一个查全部的方法
            // 按 ModelName 分组（同名型号取第一个，或按需处理）
            var modelDict = allModels
                .GroupBy(m => m.ModelName)
                .ToDictionary(g => g.Key, g => g.First());

            foreach (var typeDir in Directory.GetDirectories(procedureRoot))
            {
                string typeName = Path.GetFileName(typeDir);

                foreach (var modelDir in Directory.GetDirectories(typeDir))
                {
                    string folderName = Path.GetFileName(modelDir);

                    // 已经是新格式（包含下划线且前缀是数字）则跳过
                    if (IsNewFormat(folderName)) continue;

                    // 根据文件夹名（型号名）找到对应的数据库记录
                    if (!modelDict.TryGetValue(folderName, out var model))
                    {
                        NlogHelper.Default.Warn($"迁移跳过：找不到型号记录 [{folderName}]");
                        continue;
                    }

                    string newFolderName = ProductPathHelper.BuildProductFolderName(model.ID, model.ModelName);
                    string newPath = Path.Combine(typeDir, newFolderName);

                    if (Directory.Exists(newPath))
                    {
                        NlogHelper.Default.Warn($"目标路径已存在，跳过：{newPath}");
                        continue;
                    }

                    Directory.Move(modelDir, newPath);
                    NlogHelper.Default.Info($"迁移成功：{folderName} → {newFolderName}");
                }
            }

            NlogHelper.Default.Info("Procedure 目录迁移完成");
        }
        catch (Exception ex)
        {
            NlogHelper.Default.Error("Procedure 目录迁移失败", ex);
        }
    }

    // 判断是否已经是 {数字}_{名称} 格式
    private static bool IsNewFormat(string folderName)
    {
        int idx = folderName.IndexOf('_');
        if (idx <= 0) return false;
        return int.TryParse(folderName[..idx], out _);
    }
}