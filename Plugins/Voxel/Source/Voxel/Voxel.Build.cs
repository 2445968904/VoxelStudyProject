// Copyright 2020 Phyronnaz

#define VOXEL_PLUGIN_PRO

using System.IO;
using UnrealBuildTool;

public class Voxel : ModuleRules
{
    public Voxel(ReadOnlyTargetRules Target) : base(Target)
    {
        PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;
        bLegacyPublicIncludePaths = false;
#if UE_5_4_OR_LATER
        CppStandard = CppStandardVersion.Cpp20;
#else
		CppStandard = CppStandardVersion.Cpp17;
#endif

#if UE_4_24_OR_LATER
        bUseUnity = true;
#else
        bFasterWithoutUnity = false;
#endif

        PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "Public"));
        PrivateIncludePaths.Add(Path.Combine(ModuleDirectory, "Private"));

        // For raytracing
        PrivateIncludePaths.Add(EngineDirectory + "/Shaders/Shared");
        // For HLSL translator
        PrivateIncludePaths.Add(EngineDirectory + "/Source/Runtime/Engine/Private");

        PublicDependencyModuleNames.AddRange(
            new string[]
            {
                "Core",
                "CoreUObject",
                "Engine",
                "Networking",
                "Sockets",
                "RHI",
#if UE_4_23_OR_LATER
                "PhysicsCore",
#endif
                "RenderCore",
                "Landscape",
#if UE_4_26_OR_LATER
                "DeveloperSettings",
                "TraceLog",
#endif
            }
        );

        PrivateDependencyModuleNames.AddRange(
            new string[]
            {
                "nvTessLib",
                "VoxelEmbree3",
                "HTTP",
                "Projects",
                "Slate",
                "SlateCore",
                //"VHACD", // Not used, too slow
            }
        );

        SetupModulePhysicsSupport(Target);

        if (Target.Platform == UnrealTargetPlatform.Win64)
        {
            PrivateDependencyModuleNames.Add("ForsythTriOptimizer");
        }
        PrivateDependencyModuleNames.Add("zlib");

        if (Target.Configuration == UnrealTargetConfiguration.DebugGame ||
			Target.Configuration == UnrealTargetConfiguration.Debug)
        {
            PublicDefinitions.Add("VOXEL_DEBUG=1");
        }

        PublicDefinitions.Add("VOXEL_PLUGIN_NAME=TEXT(\"VoxelPro\")");
    }
}