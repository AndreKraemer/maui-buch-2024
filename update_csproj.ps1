$files = @(
    "d:\src\github\maui-buch-2024\Kap09\ViewsSample\ViewsSample\ViewsSample.csproj",
    "d:\src\github\maui-buch-2024\Kap10\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap10\MvvmSample\MvvmSample\MvvmSample.csproj",
    "d:\src\github\maui-buch-2024\Kap11\CodeSharingDemo\CodeSharingDemo\CodeSharingDemo.csproj",
    "d:\src\github\maui-buch-2024\Kap11\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap12\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap12\ValidationDemo\ValidationDemo\ValidationDemo.csproj",
    "d:\src\github\maui-buch-2024\Kap13\CustomControlsDemo\CustomControlsDemo\CustomControlsDemo.csproj",
    "d:\src\github\maui-buch-2024\Kap13\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap14\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap14\FlyoutSample\FlyoutSample\FlyoutSample.csproj",
    "d:\src\github\maui-buch-2024\Kap14\HierarchicalSample\HierarchicalSample\HierarchicalSample.csproj",
    "d:\src\github\maui-buch-2024\Kap14\ShellSample\ShellSample\ShellSample.csproj",
    "d:\src\github\maui-buch-2024\Kap14\TabsSample\TabsSample\TabsSample.csproj",
    "d:\src\github\maui-buch-2024\Kap15\CollectionViewSamples\CollectionViewSamples\CollectionViewSamples.csproj",
    "d:\src\github\maui-buch-2024\Kap15\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap16\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap16\LocalizationSample\LocalizationSample\LocalizationSample.csproj",
    "d:\src\github\maui-buch-2024\Kap17\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap17\ImagesSample\ImagesSample\ImagesSample.csproj",
    "d:\src\github\maui-buch-2024\Kap18\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap18\StylesSample\StylesSample\StylesSample.csproj",
    "d:\src\github\maui-buch-2024\Kap19\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap19\WebserviceSample\WebserviceSample\WebserviceSample.csproj",
    "d:\src\github\maui-buch-2024\Kap20\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap20\LocalDataSample\LocalDataSample.csproj",
    "d:\src\github\maui-buch-2024\Kap21\DeviceDemo\DeviceDemo\DeviceDemo.csproj",
    "d:\src\github\maui-buch-2024\Kap21\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    "d:\src\github\maui-buch-2024\Kap23\ElVegetarianoFurio\ElVegetarianoFurio\ElVegetarianoFurio.Maui.AutomaticMigration\ElVegetarianoFurio.Maui.AutomaticMigration.csproj",
    "d:\src\github\maui-buch-2024\Kap23\ElVegetarianoFurio\ElVegetarianoFurio\ElVegetarianoFurio.Maui.ManualMigration\ElVegetarianoFurio.Maui.ManualMigration.csproj"
)

$changedCount = 0

foreach ($file in $files) {
    if (Test-Path $file) {
        $content = Get-Content $file -Raw -Encoding UTF8
        $originalContent = $content
        
        # 1. Remove Tizen comments (lines 6-7)
        $pattern1 = '(\t\t<TargetFrameworks Condition="\$\(\[MSBuild\]::IsOSPlatform\(''windows''\)\)">\$\(TargetFrameworks\);net10\.0-windows10\.0\.19041\.0</TargetFrameworks>)[\r\n]+\t\t<!-- Uncomment to also build the tizen app\. You will need to install tizen by following this: https://github\.com/Samsung/Tizen\.NET -->[\r\n]+\t\t<!-- <TargetFrameworks>\$\(TargetFrameworks\);net10\.0-tizen</TargetFrameworks> -->'
        $content = $content -replace $pattern1, '$1'
        
        # 2. Update NoWarn comment and value  
        $pattern2 = '<!-- https://github\.com/CommunityToolkit/Maui/issues/2205 -->[\r\n]+(\t\t)<NoWarn>XC0103</NoWarn>'
        $replacement2 = "<!-- https://github.com/CommunityToolkit/Maui/issues/2921 -->`r`n" + '$1<NoWarn>NU1608</NoWarn>'
        $content = $content -replace $pattern2, $replacement2
        
        # 3. Remove Tizen SupportedOSPlatformVersion
        $pattern3 = '[\r\n]+\t\t<SupportedOSPlatformVersion Condition="\$\(\[MSBuild\]::GetTargetPlatformIdentifier\(''\$\(TargetFramework\)''\)\) == ''tizen''">6\.5</SupportedOSPlatformVersion>'
        $content = $content -replace $pattern3, ''
        
        # 4. Replace Compatibility with Essentials
        $pattern4 = '<PackageReference Include="Microsoft\.Maui\.Controls\.Compatibility" Version="\$\(MauiVersion\)" />'
        $replacement4 = '<PackageReference Include="Microsoft.Maui.Essentials" Version="$(MauiVersion)" />'
        $content = $content -replace $pattern4, $replacement4
        
        if ($content -ne $originalContent) {
            Set-Content $file -Value $content -NoNewline -Encoding UTF8
            $changedCount++
            $projectName = Split-Path (Split-Path $file -Parent) -Leaf
            Write-Host "✓ Updated: $projectName" -ForegroundColor Green
        }
    } else {
        Write-Host "⚠ File not found: $file" -ForegroundColor Yellow
    }
}

Write-Host "`n✓ Successfully updated $changedCount files" -ForegroundColor Green
