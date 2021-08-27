---@class ModelCodeGenHandler 非热更层代码生成器
local ModelCodeGenHandler = {}

--- 执行生成非热更层代码
---@param handler CS.FairyEditor.PublishHandler
---@param codeGenConfig CodeGenConfig
function ModelCodeGenHandler.Do(handler, codeGenConfig)
    local codePkgName = handler:ToFilename(handler.pkg.name); --convert chinese to pinyin, remove special chars etc.
    
    --- 从自定义配置中读取路径和命名空间
    local exportCodePath = codeGenConfig.ModelCodeOutPutPath .. '/' .. codePkgName
    local namespaceName = codeGenConfig.ModelNameSpace

    --- 从FGUI编辑器中读取配置
    ---@type CS.FairyEditor.GlobalPublishSettings.CodeGenerationConfig
    local settings = handler.project:GetSettings("Publish").codeGeneration
    local getMemberByName = settings.getMemberByName

    --- 所有将要导出的类
    ---@type CS.FairyEditor.PublishHandler.ClassInfo[]
    local allClassesTobeExport = handler:CollectClasses(settings.ignoreNoname, settings.ignoreNoname, nil)
    handler:SetupCodeFolder(exportCodePath, "cs") --check if target folder exists, and delete old files

    local allClassesTobeExportCount = allClassesTobeExport.Count
    local writer = CodeWriter.new()
    for i = 0, allClassesTobeExportCount - 1 do
        local classInfo = allClassesTobeExport[i]
        local members = classInfo.members
        writer:reset()

        writer:writeln('using FairyGUI;')
        writer:writeln('using FairyGUI.Utils;')
        writer:writeln()
        writer:writeln('namespace %s', namespaceName)
        writer:startBlock()
        writer:writeln('public partial class %s : %s', classInfo.className, classInfo.superClassName)
        writer:startBlock()

        local memberCnt = members.Count
        for j = 0, memberCnt - 1 do
            local memberInfo = members[j]
            writer:writeln('public %s %s;', memberInfo.type, memberInfo.varName)
        end
        writer:writeln('public const string URL = "ui://%s%s";', handler.pkg.id, classInfo.resId)
        writer:writeln()

        writer:writeln('public static %s CreateInstance()', classInfo.className)
        writer:startBlock()
        writer:writeln('return (%s)UIPackage.CreateObject("%s", "%s", typeof(%s));', classInfo.className, handler.pkg.name, classInfo.resName, classInfo.className)
        writer:endBlock()
        writer:writeln()

        if handler.project.type == ProjectType.MonoGame then
            writer:writeln("protected override void OnConstruct()")
            writer:startBlock()
        else
            writer:writeln('public override void ConstructFromXML(XML xml)')
            writer:startBlock()
            writer:writeln('base.ConstructFromXML(xml);')
            writer:writeln()
        end
        for j = 0, memberCnt - 1 do
            local memberInfo = members[j]
            if memberInfo.group == 0 then
                if getMemberByName then
                    writer:writeln('%s = (%s)GetChild("%s");', memberInfo.varName, memberInfo.type, memberInfo.name)
                else
                    writer:writeln('%s = (%s)GetChildAt(%s);', memberInfo.varName, memberInfo.type, memberInfo.index)
                end
            elseif memberInfo.group == 1 then
                if getMemberByName then
                    writer:writeln('%s = GetController("%s");', memberInfo.varName, memberInfo.name)
                else
                    writer:writeln('%s = GetControllerAt(%s);', memberInfo.varName, memberInfo.index)
                end
            else
                if getMemberByName then
                    writer:writeln('%s = GetTransition("%s");', memberInfo.varName, memberInfo.name)
                else
                    writer:writeln('%s = GetTransitionAt(%s);', memberInfo.varName, memberInfo.index)
                end
            end
        end
        writer:endBlock()

        writer:endBlock() --class
        writer:endBlock() --namepsace

        writer:save(exportCodePath .. '/' .. classInfo.className .. '.cs')
    end

    writer:reset()
end

return ModelCodeGenHandler