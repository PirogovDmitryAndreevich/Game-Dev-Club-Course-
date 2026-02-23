public class Trophy : Interactable, IItem
{

    private void OnEnable()
    {
        Moving();
    }

    public void Collect()
    {
        Destroy(gameObject);
    }

}
