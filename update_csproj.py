#!/usr/bin/env python3
import re
import os

files = [
    r"d:\src\github\maui-buch-2024\Kap09\ViewsSample\ViewsSample\ViewsSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap10\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap10\MvvmSample\MvvmSample\MvvmSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap11\CodeSharingDemo\CodeSharingDemo\CodeSharingDemo.csproj",
    r"d:\src\github\maui-buch-2024\Kap11\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap12\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap12\ValidationDemo\ValidationDemo\ValidationDemo.csproj",
    r"d:\src\github\maui-buch-2024\Kap13\CustomControlsDemo\CustomControlsDemo\CustomControlsDemo.csproj",
    r"d:\src\github\maui-buch-2024\Kap13\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap14\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap14\FlyoutSample\FlyoutSample\FlyoutSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap14\HierarchicalSample\HierarchicalSample\HierarchicalSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap14\ShellSample\ShellSample\ShellSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap14\TabsSample\TabsSample\TabsSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap15\CollectionViewSamples\CollectionViewSamples\CollectionViewSamples.csproj",
    r"d:\src\github\maui-buch-2024\Kap15\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap16\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap16\LocalizationSample\LocalizationSample\LocalizationSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap17\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap17\ImagesSample\ImagesSample\ImagesSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap18\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap18\StylesSample\StylesSample\StylesSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap19\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap19\WebserviceSample\WebserviceSample\WebserviceSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap20\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap20\LocalDataSample\LocalDataSample.csproj",
    r"d:\src\github\maui-buch-2024\Kap21\DeviceDemo\DeviceDemo\DeviceDemo.csproj",
    r"d:\src\github\maui-buch-2024\Kap21\DontLetMeExpire\DontLetMeExpire\DontLetMeExpire.csproj",
    r"d:\src\github\maui-buch-2024\Kap23\ElVegetarianoFurio\ElVegetarianoFurio\ElVegetarianoFurio.Maui.AutomaticMigration\ElVegetarianoFurio.Maui.AutomaticMigration.csproj",
    r"d:\src\github\maui-buch-2024\Kap23\ElVegetarianoFurio\ElVegetarianoFurio\ElVegetarianoFurio.Maui.ManualMigration\ElVegetarianoFurio.Maui.ManualMigration.csproj",
]

changed_count = 0

for file_path in files:
    if not os.path.exists(file_path):
        print(f"⚠ File not found: {file_path}")
        continue
    
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()
    
    original_content = content
    
    # 1. Remove Tizen comments (lines 6-7)
    content = re.sub(
        r'(\t\t<TargetFrameworks Condition="\$\(\[MSBuild\]::IsOSPlatform\(\'windows\'\)\)">\$\(TargetFrameworks\);net10\.0-windows10\.0\.19041\.0</TargetFrameworks>)\r?\n\t\t<!-- Uncomment to also build the tizen app\. You will need to install tizen by following this: https://github\.com/Samsung/Tizen\.NET -->\r?\n\t\t<!-- <TargetFrameworks>\$\(TargetFrameworks\);net10\.0-tizen</TargetFrameworks> -->',
        r'\1',
        content
    )
    
    # 2. Update NoWarn comment and value
    content = re.sub(
        r'<!-- https://github\.com/CommunityToolkit/Maui/issues/2205 -->\r?\n(\t\t)<NoWarn>XC0103</NoWarn>',
        r'<!-- https://github.com/CommunityToolkit/Maui/issues/2921 -->\n\1<NoWarn>NU1608</NoWarn>',
        content
    )
    
    # 3. Remove Tizen SupportedOSPlatformVersion
    content = re.sub(
        r'\r?\n\t\t<SupportedOSPlatformVersion Condition="\$\(\[MSBuild\]::GetTargetPlatformIdentifier\(\'\$\(TargetFramework\)\'\)\) == \'tizen\'">6\.5</SupportedOSPlatformVersion>',
        '',
        content
    )
    
    # 4. Replace Compatibility with Essentials
    content = re.sub(
        r'<PackageReference Include="Microsoft\.Maui\.Controls\.Compatibility" Version="\$\(MauiVersion\)" />',
        r'<PackageReference Include="Microsoft.Maui.Essentials" Version="$(MauiVersion)" />',
        content
    )
    
    if content != original_content:
        with open(file_path, 'w', encoding='utf-8', newline='') as f:
            f.write(content)
        changed_count += 1
        print(f"✓ Updated: {os.path.basename(os.path.dirname(file_path))}")

print(f"\n✓ Successfully updated {changed_count} files")
