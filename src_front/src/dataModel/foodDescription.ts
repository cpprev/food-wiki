export interface FoodDescription
{
    id: string,
    name: string,
    description: string,
    calory: number,
    nutriScore: string,
    similarFoodId: string[]
    pros: { [id: string] : string; }
    cons: { [id: string] : string; }
    articles: { [id: string] : string; }
}