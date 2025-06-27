namespace EzCheckout.Content.Diagnostics;

using System.Diagnostics;

/// <summary>
/// 
/// </summary>
public static class Diagnostics {
    // ---------- Constants ----------

    /// <summary>
    /// The name of the activity source used for diagnostics in the EzCheckout service.
    /// </summary>
    public const string ActivitySourceName = "EzCheckout.Service";


    // ---------- Internal fields ----------

    /// <summary>
    /// Gets the activity source used for diagnostics in the EzCheckout service.
    /// </summary>
    internal static ActivitySource ActivitySource {
        get;
    } = new(ActivitySourceName, "1.0.0");
}
