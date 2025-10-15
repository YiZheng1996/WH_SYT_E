/*
 Navicat Premium Dump SQL

 Source Server         : localhost_3306
 Source Server Type    : MySQL
 Source Server Version : 50726 (5.7.26)
 Source Host           : localhost:3306
 Source Schema         : testbenchdata

 Target Server Type    : MySQL
 Target Server Version : 50726 (5.7.26)
 File Encoding         : 65001

 Date: 13/10/2025 10:10:13
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for aboutdevice
-- ----------------------------
DROP TABLE IF EXISTS `aboutdevice`;
CREATE TABLE `aboutdevice`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL COMMENT '所属实验台ID',
  `OnTime` datetime NULL DEFAULT NULL,
  `EveryDay` int(11) NULL DEFAULT NULL,
  `Monthly` int(11) NULL DEFAULT NULL,
  `UsageTime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of aboutdevice
-- ----------------------------

-- ----------------------------
-- Table structure for controlpermissions
-- ----------------------------
DROP TABLE IF EXISTS `controlpermissions`;
CREATE TABLE `controlpermissions`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FormName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ControlName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DisplayName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PermissionIds` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CheckMode` int(11) NULL DEFAULT NULL,
  `IsDelete` int(11) NULL DEFAULT NULL,
  `DeleteTime` datetime NULL DEFAULT NULL,
  `Remark` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of controlpermissions
-- ----------------------------

-- ----------------------------
-- Table structure for deviceinspect
-- ----------------------------
DROP TABLE IF EXISTS `deviceinspect`;
CREATE TABLE `deviceinspect`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `PartType` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PartName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UseDuration` int(11) NOT NULL,
  `ActionNumber` int(11) NOT NULL,
  `IsDelete` int(11) NOT NULL,
  `DeleteTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 3 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of deviceinspect
-- ----------------------------

-- ----------------------------
-- Table structure for errstatistics
-- ----------------------------
DROP TABLE IF EXISTS `errstatistics`;
CREATE TABLE `errstatistics`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `ErrType` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ErrContent` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ErrTime` datetime(3) NOT NULL,
  `IsDelete` bit(1) NOT NULL,
  `DeleteTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of errstatistics
-- ----------------------------

-- ----------------------------
-- Table structure for loggerinfo
-- ----------------------------
DROP TABLE IF EXISTS `loggerinfo`;
CREATE TABLE `loggerinfo`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `MessTime` datetime(3) NOT NULL,
  `Level` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Message` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `UserName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MessageName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Source` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `idx_testbench`(`TestBenchID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 112 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of loggerinfo
-- ----------------------------
INSERT INTO `loggerinfo` VALUES (86, 1, '2025-10-09 19:26:12.000', 'Error', 'StackTrace:   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)\r\n   at System.IO.File.Copy(String sourceFileName, String destFileName, Boolean overwrite)\r\n   at MainUI.Service.ReportService.CopyReportFile(String fileName, String targetPath) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Service\\ReportService.cs:line 60\r\n   at MainUI.UcHMI.InitializeReport(String reportFileName) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\ucHMI.cs:line 189,Message:The process cannot access the file \'D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\bin\\Debug\\net8.0-windows\\reports\\report.xlsx\' because it is being used by another process. | 报表加载错误：', 'admin', '报表加载错误：', 'Logger');
INSERT INTO `loggerinfo` VALUES (87, 1, '2025-10-10 08:57:15.000', 'Error', 'StackTrace:   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)\r\n   at System.IO.File.Copy(String sourceFileName, String destFileName, Boolean overwrite)\r\n   at MainUI.Service.ReportService.CopyReportFile(String fileName, String targetPath)\r\n   at MainUI.UcHMI.InitializeReport(String reportFileName),Message:The process cannot access the file \'D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\bin\\Debug\\net8.0-windows\\reports\\report.xlsx\' because it is being used by another process. | 报表加载错误：', 'admin', '报表加载错误：', 'Logger');
INSERT INTO `loggerinfo` VALUES (88, 1, '2025-10-10 08:59:55.000', 'Error', 'StackTrace:   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)\r\n   at System.IO.File.Copy(String sourceFileName, String destFileName, Boolean overwrite)\r\n   at MainUI.Service.ReportService.CopyReportFile(String fileName, String targetPath)\r\n   at MainUI.UcHMI.InitializeReport(String reportFileName),Message:The process cannot access the file \'D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\bin\\Debug\\net8.0-windows\\reports\\report.xlsx\' because it is being used by another process. | 报表加载错误：', 'admin', '报表加载错误：', 'Logger');
INSERT INTO `loggerinfo` VALUES (89, 1, '2025-10-10 09:00:18.000', 'Error', 'StackTrace:   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)\r\n   at System.IO.File.Copy(String sourceFileName, String destFileName, Boolean overwrite)\r\n   at MainUI.Service.ReportService.CopyReportFile(String fileName, String targetPath)\r\n   at MainUI.UcHMI.InitializeReport(String reportFileName),Message:The process cannot access the file \'D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\bin\\Debug\\net8.0-windows\\reports\\report.xlsx\' because it is being used by another process. | 报表加载错误：', 'admin', '报表加载错误：', 'Logger');
INSERT INTO `loggerinfo` VALUES (90, 1, '2025-10-10 09:01:29.000', 'Warn', '不允许手动输入的用户名: admmin', '系统', '不允许手动输入的用户名: admmin', 'Logger');
INSERT INTO `loggerinfo` VALUES (91, 1, '2025-10-10 14:08:09.000', 'Warn', '控件[ucRole]未找到对应的权限配置', 'Test1', '控件[ucRole]未找到对应的权限配置', 'Logger');
INSERT INTO `loggerinfo` VALUES (92, 0, '2025-10-10 14:35:48.000', 'Info', '已清除保存的IP配置', '系统', '已清除保存的IP配置', 'Logger');
INSERT INTO `loggerinfo` VALUES (93, 0, '2025-10-10 14:35:48.000', 'Info', '已清除保存的IP配置', '系统', '已清除保存的IP配置', 'Logger');
INSERT INTO `loggerinfo` VALUES (94, 5, '2025-10-10 14:36:33.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)\r\n   at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)\r\n   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)\r\n   at MainUI.frmMainMenu.GetPermissionConfigurations() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 306\r\n   at MainUI.frmMainMenu.ConfigureRegularUserNodes(frmSettingMain main, Int32 userId) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 276\r\n   at MainUI.frmMainMenu.ConfigureMainDataNodes(frmSettingMain main) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 222\r\n   at MainUI.frmMainMenu.BtnMainData_Click(Object sender, EventArgs e) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 196\r\n   at System.Windows.Forms.Control.OnClick(EventArgs e)\r\n   at Sunny.UI.UIButton.OnClick(EventArgs e)\r\n   at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)\r\n   at System.Windows.Forms.Control.WndProc(Message& m)\r\n   at Sunny.UI.UIControl.WndProc(Message& m)\r\n   at System.Windows.Forms.NativeWindow.Callback(HWND hWnd, MessageId msg, WPARAM wparam, LPARAM lparam)\r\n   at Windows.Win32.PInvoke.DispatchMessage(MSG* lpMsg)\r\n   at System.Windows.Forms.Application.ComponentManager.Microsoft.Office.IMsoComponentManager.FPushMessageLoop(UIntPtr dwComponentID, msoloop uReason, Void* pvLoopData)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.Run(Form mainForm)\r\n   at MainUI.Program.StartMainApplication() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Program.cs:line 170,Message:An item with the same key has already been added. Key: ucItemConfiguration | 主应用程序启动失败：', 'Test1', '主应用程序启动失败：', 'Logger');
INSERT INTO `loggerinfo` VALUES (95, 5, '2025-10-10 14:36:33.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)\r\n   at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)\r\n   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)\r\n   at MainUI.frmMainMenu.GetPermissionConfigurations() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 306\r\n   at MainUI.frmMainMenu.ConfigureRegularUserNodes(frmSettingMain main, Int32 userId) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 276\r\n   at MainUI.frmMainMenu.ConfigureMainDataNodes(frmSettingMain main) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 222\r\n   at MainUI.frmMainMenu.BtnMainData_Click(Object sender, EventArgs e) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 196\r\n   at System.Windows.Forms.Control.OnClick(EventArgs e)\r\n   at Sunny.UI.UIButton.OnClick(EventArgs e)\r\n   at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)\r\n   at System.Windows.Forms.Control.WndProc(Message& m)\r\n   at Sunny.UI.UIControl.WndProc(Message& m)\r\n   at System.Windows.Forms.NativeWindow.Callback(HWND hWnd, MessageId msg, WPARAM wparam, LPARAM lparam)\r\n   at Windows.Win32.PInvoke.DispatchMessage(MSG* lpMsg)\r\n   at System.Windows.Forms.Application.ComponentManager.Microsoft.Office.IMsoComponentManager.FPushMessageLoop(UIntPtr dwComponentID, msoloop uReason, Void* pvLoopData)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.Run(Form mainForm)\r\n   at MainUI.Program.StartMainApplication() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Program.cs:line 170,Message:An item with the same key has already been added. Key: ucItemConfiguration | 主应用程序启动失败：', 'Test1', '主应用程序启动失败：', 'Logger');
INSERT INTO `loggerinfo` VALUES (96, 5, '2025-10-10 14:36:33.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)\r\n   at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)\r\n   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)\r\n   at MainUI.frmMainMenu.GetPermissionConfigurations() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 306\r\n   at MainUI.frmMainMenu.ConfigureRegularUserNodes(frmSettingMain main, Int32 userId) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 276\r\n   at MainUI.frmMainMenu.ConfigureMainDataNodes(frmSettingMain main) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 222\r\n   at MainUI.frmMainMenu.BtnMainData_Click(Object sender, EventArgs e) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 196\r\n   at System.Windows.Forms.Control.OnClick(EventArgs e)\r\n   at Sunny.UI.UIButton.OnClick(EventArgs e)\r\n   at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)\r\n   at System.Windows.Forms.Control.WndProc(Message& m)\r\n   at Sunny.UI.UIControl.WndProc(Message& m)\r\n   at System.Windows.Forms.NativeWindow.Callback(HWND hWnd, MessageId msg, WPARAM wparam, LPARAM lparam)\r\n   at Windows.Win32.PInvoke.DispatchMessage(MSG* lpMsg)\r\n   at System.Windows.Forms.Application.ComponentManager.Microsoft.Office.IMsoComponentManager.FPushMessageLoop(UIntPtr dwComponentID, msoloop uReason, Void* pvLoopData)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.Run(Form mainForm)\r\n   at MainUI.Program.StartMainApplication() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Program.cs:line 170,Message:An item with the same key has already been added. Key: ucItemConfiguration | 主应用程序启动失败：', 'Test1', '主应用程序启动失败：', 'Logger');
INSERT INTO `loggerinfo` VALUES (97, 5, '2025-10-10 14:38:32.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)\r\n   at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)\r\n   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)\r\n   at MainUI.frmMainMenu.GetPermissionConfigurations() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 306\r\n   at MainUI.frmMainMenu.ConfigureRegularUserNodes(frmSettingMain main, Int32 userId) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 276\r\n   at MainUI.frmMainMenu.ConfigureMainDataNodes(frmSettingMain main) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 222\r\n   at MainUI.frmMainMenu.BtnMainData_Click(Object sender, EventArgs e) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 196\r\n   at System.Windows.Forms.Control.OnClick(EventArgs e)\r\n   at Sunny.UI.UIButton.OnClick(EventArgs e)\r\n   at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)\r\n   at System.Windows.Forms.Control.WndProc(Message& m)\r\n   at Sunny.UI.UIControl.WndProc(Message& m)\r\n   at System.Windows.Forms.NativeWindow.Callback(HWND hWnd, MessageId msg, WPARAM wparam, LPARAM lparam)\r\n   at Windows.Win32.PInvoke.DispatchMessage(MSG* lpMsg)\r\n   at System.Windows.Forms.Application.ComponentManager.Microsoft.Office.IMsoComponentManager.FPushMessageLoop(UIntPtr dwComponentID, msoloop uReason, Void* pvLoopData)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.Run(Form mainForm)\r\n   at MainUI.Program.StartMainApplication() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Program.cs:line 170,Message:An item with the same key has already been added. Key: ucItemConfiguration | 主应用程序启动失败：', 'Test1', '主应用程序启动失败：', 'Logger');
INSERT INTO `loggerinfo` VALUES (98, 5, '2025-10-10 14:38:32.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)\r\n   at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)\r\n   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)\r\n   at MainUI.frmMainMenu.GetPermissionConfigurations() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 306\r\n   at MainUI.frmMainMenu.ConfigureRegularUserNodes(frmSettingMain main, Int32 userId) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 276\r\n   at MainUI.frmMainMenu.ConfigureMainDataNodes(frmSettingMain main) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 222\r\n   at MainUI.frmMainMenu.BtnMainData_Click(Object sender, EventArgs e) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 196\r\n   at System.Windows.Forms.Control.OnClick(EventArgs e)\r\n   at Sunny.UI.UIButton.OnClick(EventArgs e)\r\n   at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)\r\n   at System.Windows.Forms.Control.WndProc(Message& m)\r\n   at Sunny.UI.UIControl.WndProc(Message& m)\r\n   at System.Windows.Forms.NativeWindow.Callback(HWND hWnd, MessageId msg, WPARAM wparam, LPARAM lparam)\r\n   at Windows.Win32.PInvoke.DispatchMessage(MSG* lpMsg)\r\n   at System.Windows.Forms.Application.ComponentManager.Microsoft.Office.IMsoComponentManager.FPushMessageLoop(UIntPtr dwComponentID, msoloop uReason, Void* pvLoopData)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.Run(Form mainForm)\r\n   at MainUI.Program.StartMainApplication() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Program.cs:line 170,Message:An item with the same key has already been added. Key: ucItemConfiguration | 主应用程序启动失败：', 'Test1', '主应用程序启动失败：', 'Logger');
INSERT INTO `loggerinfo` VALUES (99, 5, '2025-10-10 14:38:32.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.TryInsert(TKey key, TValue value, InsertionBehavior behavior)\r\n   at System.Collections.Generic.Dictionary`2.Add(TKey key, TValue value)\r\n   at System.Linq.Enumerable.ToDictionary[TSource,TKey,TElement](IEnumerable`1 source, Func`2 keySelector, Func`2 elementSelector, IEqualityComparer`1 comparer)\r\n   at MainUI.frmMainMenu.GetPermissionConfigurations() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 306\r\n   at MainUI.frmMainMenu.ConfigureRegularUserNodes(frmSettingMain main, Int32 userId) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 276\r\n   at MainUI.frmMainMenu.ConfigureMainDataNodes(frmSettingMain main) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 222\r\n   at MainUI.frmMainMenu.BtnMainData_Click(Object sender, EventArgs e) in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\frmMainMenu.cs:line 196\r\n   at System.Windows.Forms.Control.OnClick(EventArgs e)\r\n   at Sunny.UI.UIButton.OnClick(EventArgs e)\r\n   at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)\r\n   at System.Windows.Forms.Control.WndProc(Message& m)\r\n   at Sunny.UI.UIControl.WndProc(Message& m)\r\n   at System.Windows.Forms.NativeWindow.Callback(HWND hWnd, MessageId msg, WPARAM wparam, LPARAM lparam)\r\n   at Windows.Win32.PInvoke.DispatchMessage(MSG* lpMsg)\r\n   at System.Windows.Forms.Application.ComponentManager.Microsoft.Office.IMsoComponentManager.FPushMessageLoop(UIntPtr dwComponentID, msoloop uReason, Void* pvLoopData)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(msoloop reason, ApplicationContext context)\r\n   at System.Windows.Forms.Application.Run(Form mainForm)\r\n   at MainUI.Program.StartMainApplication() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Program.cs:line 170,Message:An item with the same key has already been added. Key: ucItemConfiguration | 主应用程序启动失败：', 'Test1', '主应用程序启动失败：', 'Logger');
INSERT INTO `loggerinfo` VALUES (100, 5, '2025-10-10 14:44:36.000', 'Warn', '控件[ucRole]未找到对应的权限配置', 'Test1', '控件[ucRole]未找到对应的权限配置', 'Logger');
INSERT INTO `loggerinfo` VALUES (101, 5, '2025-10-10 14:44:56.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.get_Item(TKey key)\r\n   at MainUI.Procedure.User.ucPermissionAllocation.GetPermissionChecks() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Procedure\\User\\ucPermissionAllocation.cs:line 94,Message:The given key \'21\' was not present in the dictionary. | 获取权限Check发生错误：{0}', 'admin', '获取权限Check发生错误：{0}', 'Logger');
INSERT INTO `loggerinfo` VALUES (102, 5, '2025-10-10 14:45:19.000', 'Error', 'StackTrace:   at System.Collections.Generic.Dictionary`2.get_Item(TKey key)\r\n   at MainUI.Procedure.User.ucPermissionAllocation.GetPermissionChecks() in D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\Procedure\\User\\ucPermissionAllocation.cs:line 94,Message:The given key \'20\' was not present in the dictionary. | 获取权限Check发生错误：{0}', 'admin', '获取权限Check发生错误：{0}', 'Logger');
INSERT INTO `loggerinfo` VALUES (103, 5, '2025-10-10 14:47:33.000', 'Warn', '控件[ucRole]未找到对应的权限配置', 'Test1', '控件[ucRole]未找到对应的权限配置', 'Logger');
INSERT INTO `loggerinfo` VALUES (104, 5, '2025-10-10 16:41:10.000', 'Error', 'StackTrace:   at MainUI.BLL.PermissionBLL.AddPermission(PermissionModel model)\r\n   at MainUI.Procedure.Edit.frmPermissionEdit.BtnSubmit_Click(Object sender, EventArgs e),Message:权限代码 \'A123\' 已存在，请使用其他代码！ | 保存权限失败：权限代码 \'A123\' 已存在，请使用其他代码！', 'admin', '保存权限失败：权限代码 \'A123\' 已存在，请使用其他代码！', 'Logger');
INSERT INTO `loggerinfo` VALUES (105, 5, '2025-10-10 16:41:10.000', 'Error', 'StackTrace:   at MainUI.BLL.PermissionBLL.AddPermission(PermissionModel model)\r\n   at MainUI.Procedure.Edit.frmPermissionEdit.BtnSubmit_Click(Object sender, EventArgs e),Message:权限代码 \'A123\' 已存在，请使用其他代码！ | 保存权限失败：权限代码 \'A123\' 已存在，请使用其他代码！', 'admin', '保存权限失败：权限代码 \'A123\' 已存在，请使用其他代码！', 'Logger');
INSERT INTO `loggerinfo` VALUES (106, 5, '2025-10-10 16:41:14.000', 'Error', 'StackTrace:   at MainUI.BLL.PermissionBLL.AddPermission(PermissionModel model),Message:控件名称 \'aaa123\' 已存在，请使用其他名称！ | 添加权限失败：控件名称 \'aaa123\' 已存在，请使用其他名称！', 'admin', '添加权限失败：控件名称 \'aaa123\' 已存在，请使用其他名称！', 'Logger');
INSERT INTO `loggerinfo` VALUES (107, 5, '2025-10-10 16:41:14.000', 'Error', 'StackTrace:   at MainUI.BLL.PermissionBLL.AddPermission(PermissionModel model)\r\n   at MainUI.Procedure.Edit.frmPermissionEdit.BtnSubmit_Click(Object sender, EventArgs e),Message:控件名称 \'aaa123\' 已存在，请使用其他名称！ | 保存权限失败：控件名称 \'aaa123\' 已存在，请使用其他名称！', 'admin', '保存权限失败：控件名称 \'aaa123\' 已存在，请使用其他名称！', 'Logger');
INSERT INTO `loggerinfo` VALUES (108, 5, '2025-10-10 16:42:05.000', 'Error', 'StackTrace:   at MainUI.BLL.PermissionBLL.AddPermission(PermissionModel model),Message:控件名称 \'ucPermissionAllocation\' 已存在，请使用其他名称！ | 添加权限失败：控件名称 \'ucPermissionAllocation\' 已存在，请使用其他名称！', 'admin', '添加权限失败：控件名称 \'ucPermissionAllocation\' 已存在，请使用其他名称！', 'Logger');
INSERT INTO `loggerinfo` VALUES (109, 5, '2025-10-10 16:42:05.000', 'Error', 'StackTrace:   at MainUI.BLL.PermissionBLL.AddPermission(PermissionModel model)\r\n   at MainUI.Procedure.Edit.frmPermissionEdit.BtnSubmit_Click(Object sender, EventArgs e),Message:控件名称 \'ucPermissionAllocation\' 已存在，请使用其他名称！ | 保存权限失败：控件名称 \'ucPermissionAllocation\' 已存在，请使用其他名称！', 'admin', '保存权限失败：控件名称 \'ucPermissionAllocation\' 已存在，请使用其他名称！', 'Logger');
INSERT INTO `loggerinfo` VALUES (110, 5, '2025-10-11 15:22:32.000', 'Error', 'StackTrace:   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)\r\n   at System.IO.File.Copy(String sourceFileName, String destFileName, Boolean overwrite)\r\n   at MainUI.Service.ReportService.CopyReportFile(String fileName, String targetPath)\r\n   at MainUI.UcHMI.InitializeReport(String reportFileName),Message:Access to the path \'D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\bin\\Debug\\net8.0-windows\\reports\\report.xls\' is denied. | 报表加载错误：', 'admin', '报表加载错误：', 'Logger');
INSERT INTO `loggerinfo` VALUES (111, 5, '2025-10-11 15:43:29.000', 'Error', 'StackTrace:   at System.IO.FileSystem.CopyFile(String sourceFullPath, String destFullPath, Boolean overwrite)\r\n   at System.IO.File.Copy(String sourceFileName, String destFileName, Boolean overwrite)\r\n   at MainUI.Service.ReportService.CopyReportFile(String fileName, String targetPath)\r\n   at MainUI.UcHMI.InitializeReport(String reportFileName),Message:Access to the path \'D:\\易峥\\2025\\2025-09\\武汉软件通用模板\\src\\master\\MainUI\\bin\\Debug\\net8.0-windows\\reports\\report.xls\' is denied. | 报表加载错误：', 'admin', '报表加载错误：', 'Logger');

-- ----------------------------
-- Table structure for meteringremind
-- ----------------------------
DROP TABLE IF EXISTS `meteringremind`;
CREATE TABLE `meteringremind`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `InspectType` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `InspectName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CurrentTime` datetime(3) NOT NULL,
  `NextTime` datetime(3) NOT NULL,
  `InspectDescribe` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Cycle` int(11) NOT NULL,
  `IsDelete` bit(1) NOT NULL,
  `DeleteTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 14 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of meteringremind
-- ----------------------------

-- ----------------------------
-- Table structure for modelstable
-- ----------------------------
DROP TABLE IF EXISTS `modelstable`;
CREATE TABLE `modelstable`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `TypeID` int(11) NOT NULL,
  `Mark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateTime` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UpdateTime` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `DrawingNo` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsRelease` bit(1) NOT NULL,
  `ReleaseTime` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 35 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of modelstable
-- ----------------------------
INSERT INTO `modelstable` VALUES (34, 'ASV11', 17, '', '2025-10-11 11:34:14', NULL, b'0', '', b'1', '2025-10-11 11:44:11');
INSERT INTO `modelstable` VALUES (33, '3/4', 16, '', '2025-10-11 10:12:27', NULL, b'0', '', b'1', '2025-10-11 11:52:25');
INSERT INTO `modelstable` VALUES (32, '测试型号02', 14, '', '2025-10-09 15:10:46', NULL, b'0', '002', b'1', '2025-10-09 15:10:49');
INSERT INTO `modelstable` VALUES (31, '测试型号01', 14, '', '2025-10-09 15:10:39', NULL, b'0', '001', b'0', '未发布');

-- ----------------------------
-- Table structure for modeltypetable
-- ----------------------------
DROP TABLE IF EXISTS `modeltypetable`;
CREATE TABLE `modeltypetable`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ModelTypeName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Mark` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `TestBenchID` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 18 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of modeltypetable
-- ----------------------------
INSERT INTO `modeltypetable` VALUES (16, '止回阀', '', 5);
INSERT INTO `modeltypetable` VALUES (15, '调压阀', '', 5);
INSERT INTO `modeltypetable` VALUES (14, '测试类型', '', 1);
INSERT INTO `modeltypetable` VALUES (17, '滑行装置', '', 2);

-- ----------------------------
-- Table structure for permission
-- ----------------------------
DROP TABLE IF EXISTS `permission`;
CREATE TABLE `permission`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `PermissionName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PermissionCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ControlName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `PermissionType` int(11) NULL DEFAULT NULL,
  `PermissionNotes` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDelete` int(11) NOT NULL,
  `DeleteTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 26 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of permission
-- ----------------------------
INSERT INTO `permission` VALUES (3, '历史报表', 'history', 'btnReports', 0, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (4, '硬件校准', 'hardware', 'btnHardwareTest', 1, '硬件校准', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (5, '参数管理', 'data', 'btnMainData', 1, '各种参数', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (6, '设备检查', 'deviceInspect', 'btnDeviceDetection', 3, '', 0, '2025-09-17 15:17:13.000');
INSERT INTO `permission` VALUES (7, '维保计量', 'meteringremind', 'btnMeteringRemind', 0, '', 0, '2025-09-17 15:17:17.000');
INSERT INTO `permission` VALUES (8, '问题统计', 'errStatistics', 'btnErrStatistics', 1, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (9, '日志管理', 'nlog', 'btnNLog', 2, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (10, '用户管理', 'user', 'ucUserManager', 2, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (11, '试验参数', 'TestParams', 'ucTestParams', 2, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (12, '更改密码', 'changepwd', 'btnChangePwd', 1, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (14, '项点配置', 'tempoint', 'ucItemConfiguration', 2, '', 0, '2025-10-10 14:34:38.401');
INSERT INTO `permission` VALUES (15, '打印报告', 'PrintReport', 'btnPrintReport', 2, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (17, '类型维护', 'ModelType', 'ucKindManage', 0, '', 0, '0001-01-01 00:00:00.000');
INSERT INTO `permission` VALUES (18, '型号维护', 'Model', 'ucModelManage', NULL, NULL, 0, '2025-09-18 10:57:43.000');
INSERT INTO `permission` VALUES (21, '项点管理', 'ItemManagerial', 'ucItemManagerial', NULL, NULL, 0, '2025-09-18 10:57:43.000');
INSERT INTO `permission` VALUES (22, '保存报表', 'SaveReport', 'btnSaveReport', NULL, '', 0, '0001-01-01 00:00:00.000');

-- ----------------------------
-- Table structure for record
-- ----------------------------
DROP TABLE IF EXISTS `record`;
CREATE TABLE `record`  (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `KindID` int(11) NOT NULL,
  `ModelID` int(11) NOT NULL,
  `TestID` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Tester` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `TestTime` datetime(3) NOT NULL,
  `ReportPath` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsResult` int(11) NULL DEFAULT NULL,
  `TestDuration` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  INDEX `idx_testbench`(`TestBenchID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of record
-- ----------------------------

-- ----------------------------
-- Table structure for role
-- ----------------------------
DROP TABLE IF EXISTS `role`;
CREATE TABLE `role`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Describe` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDelete` int(11) NOT NULL,
  `DeleteTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of role
-- ----------------------------
INSERT INTO `role` VALUES (1, '系统管理员', 'admin', 0, '0001-01-01 00:00:00.000');
INSERT INTO `role` VALUES (2, '技术员', 'craft', 0, '0001-01-01 00:00:00.000');
INSERT INTO `role` VALUES (3, '操作员', 'operator', 0, '0001-01-01 00:00:00.000');

-- ----------------------------
-- Table structure for testbench
-- ----------------------------
DROP TABLE IF EXISTS `testbench`;
CREATE TABLE `testbench`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `BenchCode` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BenchName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Location` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Status` bit(1) NOT NULL,
  `BenchIP` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `DataScope` int(11) NOT NULL,
  `CreateTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE INDEX `uk_bench_code`(`BenchCode`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of testbench
-- ----------------------------
INSERT INTO `testbench` VALUES (1, 'WHYT-A', '制动阀类试验台A', '实验室A区', b'1', '192.168.0.189', 1, '2025-10-07 10:58:56.000');
INSERT INTO `testbench` VALUES (2, 'WHYT-B', '制动阀类试验台B', '实验室B区', b'1', '192.168.6.180', 0, '2025-10-07 10:58:56.000');
INSERT INTO `testbench` VALUES (3, 'WHYT-C', '制动阀类试验台C', '实验室C区', b'1', '192.168.0.102', 0, '2025-10-07 10:58:56.000');
INSERT INTO `testbench` VALUES (4, 'WHYT-D', '制动阀类试验台D', '实验室D区', b'1', '192.168.0.103', 0, '2025-10-07 10:58:56.000');
INSERT INTO `testbench` VALUES (5, 'WHYT-E', '制动阀类试验台E', '实验室E区', b'1', '192.168.0.211', 0, '2025-10-07 10:58:56.000');

-- ----------------------------
-- Table structure for testprocess
-- ----------------------------
DROP TABLE IF EXISTS `testprocess`;
CREATE TABLE `testprocess`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `ProcessName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EntityClassName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ModelTypeID` int(11) NOT NULL,
  `TestBenchID` int(11) NOT NULL,
  `IsVisible` bit(1) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 54 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of testprocess
-- ----------------------------
INSERT INTO `testprocess` VALUES (49, '容量试验-2号阀', 'WSP_2CapacityTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (48, '滑行检测作用试验-2号阀', 'WSP_2RollingTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (47, '泄漏试验-2号阀', 'WSP_2LeakTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (46, '动作试验-2号阀', 'WSP_2ActionTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (45, '电阻测试-2号阀', 'WSP_2ResistanceTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (44, '绝缘耐压试验-1号阀', 'WSP_1InsulationTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (43, '容量试验-1号阀', 'WSP_1CapacityTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (42, '滑行检测作用试验-1号阀', 'WSP_1RollingTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (41, '泄漏试验-1号阀', 'WSP_1LeakTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (40, '动作试验-1号阀', 'WSP_1ActionTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (39, '电阻测试-1号阀', 'WSP_1ResistanceTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (38, '测试项点02', 'Test02', 14, 1, b'1');
INSERT INTO `testprocess` VALUES (37, '测试项点01', 'Test01', 14, 1, b'1');
INSERT INTO `testprocess` VALUES (50, '绝缘耐压试验-2号阀', 'WSP_2InsulationTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (51, '整体泄漏试验', 'WSP_OverallLeakageTest', 17, 2, b'1');
INSERT INTO `testprocess` VALUES (52, '高压泄漏试验', 'HighPressure', 16, 5, b'1');
INSERT INTO `testprocess` VALUES (53, '低压泄漏试验', 'LowPressure', 16, 5, b'1');

-- ----------------------------
-- Table structure for teststep
-- ----------------------------
DROP TABLE IF EXISTS `teststep`;
CREATE TABLE `teststep`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `Step` int(11) NOT NULL,
  `ProcessName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ModelID` int(11) NOT NULL,
  `TestProcessID` int(11) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `idx_testbench`(`TestBenchID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 165 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of teststep
-- ----------------------------
INSERT INTO `teststep` VALUES (164, 5, 1, '高压泄漏试验', 33, 52);
INSERT INTO `teststep` VALUES (163, 5, 0, '低压泄漏试验', 33, 53);
INSERT INTO `teststep` VALUES (162, 2, 12, '整体泄漏试验', 34, 51);
INSERT INTO `teststep` VALUES (161, 2, 11, '绝缘耐压试验-2号阀', 34, 50);
INSERT INTO `teststep` VALUES (160, 2, 10, '容量试验-2号阀', 34, 49);
INSERT INTO `teststep` VALUES (159, 2, 9, '滑行检测作用试验-2号阀', 34, 48);
INSERT INTO `teststep` VALUES (158, 2, 8, '泄漏试验-2号阀', 34, 47);
INSERT INTO `teststep` VALUES (157, 2, 7, '动作试验-2号阀', 34, 46);
INSERT INTO `teststep` VALUES (156, 2, 6, '电阻测试-2号阀', 34, 45);
INSERT INTO `teststep` VALUES (155, 2, 5, '绝缘耐压试验-1号阀', 34, 44);
INSERT INTO `teststep` VALUES (154, 2, 4, '容量试验-1号阀', 34, 43);
INSERT INTO `teststep` VALUES (153, 2, 3, '滑行检测作用试验-1号阀', 34, 42);
INSERT INTO `teststep` VALUES (152, 2, 2, '泄漏试验-1号阀', 34, 41);
INSERT INTO `teststep` VALUES (151, 2, 1, '动作试验-1号阀', 34, 40);
INSERT INTO `teststep` VALUES (150, 2, 0, '电阻测试-1号阀', 34, 39);
INSERT INTO `teststep` VALUES (149, 1, 1, '测试项点02', 32, 38);
INSERT INTO `teststep` VALUES (148, 1, 0, '测试项点01', 32, 37);

-- ----------------------------
-- Table structure for user_permission
-- ----------------------------
DROP TABLE IF EXISTS `user_permission`;
CREATE TABLE `user_permission`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL DEFAULT 0 COMMENT '所属实验台ID',
  `User_id` int(11) NOT NULL,
  `Permission_id` int(11) NOT NULL,
  `IsDelete` int(11) NOT NULL,
  `DeleteTime` datetime(3) NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `idx_testbench`(`TestBenchID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 294 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Fixed;

-- ----------------------------
-- Records of user_permission
-- ----------------------------
INSERT INTO `user_permission` VALUES (275, 0, 1, 21, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (273, 0, 1, 18, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (272, 0, 1, 17, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (270, 0, 1, 15, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (269, 0, 1, 14, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (267, 0, 1, 12, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (266, 0, 1, 11, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (265, 0, 1, 10, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (264, 0, 1, 9, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (263, 0, 1, 8, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (262, 0, 1, 7, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (261, 0, 1, 6, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (260, 0, 1, 5, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (229, 0, 3, 3, 0, '0001-01-01 00:00:00.000');
INSERT INTO `user_permission` VALUES (230, 0, 3, 4, 0, '0001-01-01 00:00:00.000');
INSERT INTO `user_permission` VALUES (231, 0, 3, 5, 0, '0001-01-01 00:00:00.000');
INSERT INTO `user_permission` VALUES (232, 0, 3, 12, 0, '0001-01-01 00:00:00.000');
INSERT INTO `user_permission` VALUES (234, 0, 3, 21, 0, '0001-01-01 00:00:00.000');
INSERT INTO `user_permission` VALUES (235, 0, 3, 22, 0, '0001-01-01 00:00:00.000');
INSERT INTO `user_permission` VALUES (292, 0, 2, 12, 0, '2025-10-10 14:46:41.811');
INSERT INTO `user_permission` VALUES (291, 0, 2, 8, 0, '2025-10-10 14:46:41.811');
INSERT INTO `user_permission` VALUES (290, 0, 2, 5, 0, '2025-10-10 14:46:41.811');
INSERT INTO `user_permission` VALUES (259, 0, 1, 4, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (258, 0, 1, 3, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (276, 0, 1, 22, 0, '2025-10-09 15:07:56.931');
INSERT INTO `user_permission` VALUES (289, 0, 2, 4, 0, '2025-10-10 14:46:41.811');
INSERT INTO `user_permission` VALUES (288, 0, 2, 3, 0, '2025-10-10 14:46:41.811');

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TestBenchID` int(11) NOT NULL,
  `Username` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `JobNumber` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Role_ID` int(11) NOT NULL,
  `Sort` int(11) NOT NULL DEFAULT 0,
  `Enabeld` int(11) NOT NULL DEFAULT 1,
  `CreateUsername` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateDateTime` int(11) NULL DEFAULT NULL,
  `UpdateDateTime` int(11) NULL DEFAULT NULL,
  `LoginTime` int(11) NULL DEFAULT NULL,
  `LastLoginTime` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE,
  INDEX `idx_testbench`(`TestBenchID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 5 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES (1, 0, 'admin', '1', NULL, 1, 0, 1, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `users` VALUES (2, 0, 'Test1', '1', NULL, 2, 0, 1, NULL, NULL, NULL, NULL, NULL);
INSERT INTO `users` VALUES (3, 0, 'Test2', '1', NULL, 3, 0, 1, NULL, NULL, NULL, NULL, NULL);

SET FOREIGN_KEY_CHECKS = 1;
