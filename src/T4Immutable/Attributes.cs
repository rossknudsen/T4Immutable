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
    ExcludeEquals = 1,

    /// <summary>
    /// Do not generate a GetHashCode() implementation.
    /// </summary>
    ExcludeGetHashCode = 2,

    /// <summary>
    /// Generate operator == and operator !=.
    /// </summary>
    IncludeOperatorEquals = 4,

    /// <summary>
    /// Do not generated a ToString() implementation.
    /// </summary>
    ExcludeToString = 8,

    /// <summary>
    /// Do not generate a With() implementation.
    /// </summary>
    ExcludeWith = 16
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
  /// String to add before the constructor parameter.
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
