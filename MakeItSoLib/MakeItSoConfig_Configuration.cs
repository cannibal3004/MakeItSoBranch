using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MakeItSoLib
{
    /// <summary>
    /// Holds config for a configuration (debug / release) in a project.
    /// </summary>
    public class MakeItSoConfig_Configuration
    {
        #region Public methods and properties

        /// <summary>
        /// Constructor
        /// </summary>
        public MakeItSoConfig_Configuration(MakeItSoConfig_Project projectConfig,
                                            MakeItSoConfig_Configuration parentConfig)
        {
            m_projectConfig = projectConfig;
            m_parentConfig = parentConfig;
        }

        /// <summary>
        /// The 'parent' project config.
        /// </summary>
        public MakeItSoConfig_Project ProjectConfig
        {
            get { return m_projectConfig; }
        }

        /// <summary>
        /// Adds a library to the collection to be added to the
        /// configuration we're managing.
        /// </summary>
        public void addLibraryToAdd(string libraryName)
        {
            m_librariesToAdd.Add(libraryName);
        }

        /// <summary>
        /// Returns the collection of libraries to add to this configuration.
        /// </summary>
        public List<string> getLibrariesToAdd()
        {
            return getInheritedHSet(c => c.m_librariesToAdd);
        }

        /// <summary>
        /// Adds a library-path to the collection to be added to the
        /// configuration we're managing.
        /// </summary>
        public void addLibraryPathToAdd(string libraryPath)
        {
            //string absolutePath = Path.Combine(ProjectConfig.SolutionConfig.SolutionRootFolder, libraryPath);
            //absolutePath = Path.GetFullPath(absolutePath);
            //m_libraryPathsToAdd.Add(absolutePath);
            m_libraryPathsToAdd.Add(libraryPath);
        }

        /// <summary>
        /// Adds a include-path to the collection to be added to the
        /// configuration we're managing.
        /// </summary>
        public void addIncludePathToAdd(string includePath)
        {
            //string absolutePath = Path.Combine(ProjectConfig.SolutionConfig.SolutionRootFolder, includePath);
            //absolutePath = Path.GetFullPath(absolutePath);
            //m_includePathsToAdd.Add(absolutePath);
            m_includePathsToAdd.Add(includePath);
        }

        /// <summary>
        /// Returns the collection of library paths to add to this configuration.
        /// </summary>
        public List<string> getLibraryPathsToAdd()
        {
            return getInheritedHSet(c => c.m_libraryPathsToAdd);
        }

        /// <summary>
        /// Returns the collection of include paths to add to this configuration.
        /// </summary>
        public List<string> getIncludePathsToAdd()
        {
            return getInheritedHSet(c => c.m_includePathsToAdd);
        }

        /// <summary>
        /// Adds a preprocessor-definition to the configuration.
        /// </summary>
        public void addPreprocessorDefinitionToAdd(string definition)
        {
            m_preprocessorDefinitionsToAdd.Add(definition);
        }

        /// <summary>
        /// Gets the collection of preprocessor-definitions to add.
        /// </summary>
        public List<string> getPreprocessorDefinitionsToAdd()
        {
            return getInheritedHSet(c => c.m_preprocessorDefinitionsToAdd);
        }

        /// <summary>
        /// Adds a compiler flag to the configuration.
        /// </summary>
        public void addCompilerFlagToAdd(string flag)
        {
            m_compilerFlagsToAdd.Add(flag);
        }

        /// <summary>
        /// Gets the collection of compiler flags to add.
        /// </summary>
        public List<string> getCompilerFlagsToAdd()
        {
            return getInheritedHSet(c => c.m_compilerFlagsToAdd);
        }

        #endregion

        private delegate HashSet<string> HSetFunc(MakeItSoConfig_Configuration config);
        private List<string> getInheritedHSet(HSetFunc func)
        {
            HashSet<string> stringSet = new HashSet<string>(func(this));
            for (MakeItSoConfig_Configuration config = m_parentConfig;
                config != null;
                config = config.m_parentConfig)
                stringSet.UnionWith(func(config));
            return stringSet.ToList();
        }

        #region Private data

        // The project-config that this configuration-config is part of...
        private MakeItSoConfig_Project m_projectConfig = null;

        // Config parente.
        private MakeItSoConfig_Configuration m_parentConfig = null;

        // Libraries to be added to the configuration...
        private HashSet<string> m_librariesToAdd = new HashSet<string>();

        // Library paths to add to the configuration (stored as full paths)...
        private HashSet<string> m_libraryPathsToAdd = new HashSet<string>();

        // Include paths to add to the configuration (stored as full paths)...
        private HashSet<string> m_includePathsToAdd = new HashSet<string>();

        // Preprocessor definitions to add to the configuration...
        private HashSet<string> m_preprocessorDefinitionsToAdd = new HashSet<string>();

        // Compiler flags to add to the configuration...
        private HashSet<string> m_compilerFlagsToAdd = new HashSet<string>();

        #endregion
    }
}
