﻿using System;

// ReSharper disable RedundantAttributeUsageProperty

// note we can't use features > c#4 since it needs to be compiled by the template
// this means for example no auto-property initializers
// ReSharper disable ConvertToAutoProperty

namespace T4Immutable {

  #region For classes

  /// <summary>
  /// Marks a class so it can be processed by T4Immutable.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public sealed class ImmutableClassAttribute : Attribute {
    private ConstructorAccessLevel _constructorAccessLevel = ConstructorAccessLevel.Public;
    private ImmutableClassOptions _options = ImmutableClassOptions.None;
    private string _preConstructor;
    private BuilderAccessLevel _builderAccessLevel = BuilderAccessLevel.Public;

    /// <summary>
    /// Immutable class generation options.
    /// </summary>
    public ImmutableClassOptions Options {
      get { return _options; }
      set { _options = value; }
    }

    /// <summary>
    /// Generated constructor access level (modifier).
    /// </summary>
    public ConstructorAccessLevel ConstructorAccessLevel {
      get { return _constructorAccessLevel; }
      set { _constructorAccessLevel = value; }
    }

    /// <summary>
    /// String with code to add before the constructor.
    /// </summary>
    public string PreConstructor {
      get { return _preConstructor; }
      set { _preConstructor = value; }
    }

    /// <summary>
    /// Generated builder class access level (modifier).
    /// </summary>
    public BuilderAccessLevel BuilderAccessLevel {
      get { return _builderAccessLevel; }
      set { _builderAccessLevel = value; }
    }
  }

  /// <summary>
  /// Immutable class generation options for T4Immutable.
  /// </summary>
  [Flags]
  public enum ImmutableClassOptions {
    /// <summary>
    /// Default.
    /// </summary>
    None = 0,

    /// <summary>
    /// Do not generate Equals() implementation or add the IEquatable interface.
    /// </summary>
    ExcludeEquals = 1 << 0,

    /// <summary>
    /// Do not generate a GetHashCode() implementation.
    /// </summary>
    ExcludeGetHashCode = 1 << 1,

    /// <summary>
    /// Generate operator == and operator !=.
    /// </summary>
    IncludeOperatorEquals = 1 << 2,

    /// <summary>
    /// Do not generate a ToString() implementation.
    /// </summary>
    ExcludeToString = 1 << 3,

    /// <summary>
    /// Do not generate a With() implementation.
    /// </summary>
    ExcludeWith = 1 << 4,

    /// <summary>
    /// Do not generate a constructor.
    /// </summary>
    ExcludeConstructor = 1 << 5,

    /// <summary>
    /// Allow the user to define his own constructors.
    /// </summary>
    AllowCustomConstructors = 1 << 6,

    /// <summary>
    /// Do not generate a builder class or ImmutableToBuilder() implementation. Implies ExcludeToBuilder.
    /// </summary>
    ExcludeBuilder = 1 << 7,

    /// <summary>
    /// Do not generate a ToBuilder() implementation.
    /// </summary>
    ExcludeToBuilder = 1 << 8,
  }

  /// <summary>
  /// Access level (modifier) for constructors generated by T4Immutable.
  /// </summary>
  public enum ConstructorAccessLevel {
    Public,
    Protected,
    Internal,
    Private,
    ProtectedInternal
  }

  /// <summary>
  /// Access level (modifier) for builders generated by T4Immutable.
  /// </summary>
  public enum BuilderAccessLevel {
    Public,
    Protected,
    Internal,
    Private,
    ProtectedInternal
  }

  #endregion

  #region For properties

  /// <summary>
  /// Adds a JetBrains.Annotations.NotNull attribute to the constructor parameter.
  /// Also enables a not null precheck implicitely.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class ConstructorParamNotNullAttribute : Attribute {
  }

  /// <summary>
  /// Generate a not null check at the beginning of the constructor.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class PreNotNullCheckAttribute : Attribute {
  }

  /// <summary>
  /// Generate a not null check at the end of the constructor.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class PostNotNullCheckAttribute : Attribute {
  }

  /// <summary>
  /// Marks a property as computed, effectively making T4Immutable ignore it.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class ComputedPropertyAttribute : Attribute {
  }

  /// <summary>
  /// String with code to add before the constructor parameter.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class PreConstructorParamAttribute : Attribute {
    public string Pre { get; private set; }

    public PreConstructorParamAttribute(string pre) {
      Pre = pre;
    }
  }

  #endregion

  #region Internal

  /// <summary>
  /// Attribute used internally by T4Immutable to mark generated code. Not for public usage.
  /// </summary>
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
  public sealed class GeneratedCodeAttribute : Attribute {
  }

  #endregion
}