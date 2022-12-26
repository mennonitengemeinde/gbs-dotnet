namespace Gbs.Wasm.Store.ChurchUseCase;

public class ChurchReducers
{
    [ReducerMethod(typeof(FetchChurchesAction))]
    public static ChurchState ReduceFetchChurches(ChurchState state) =>
        new(true, state.Churches, null);
    
    [ReducerMethod]
    public static ChurchState ReduceFetchChurchesResultAction(ChurchState state, FetchChurchesActionResult action) =>
        new(false, action.Churches, action.Error);
}