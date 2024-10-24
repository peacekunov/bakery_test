export interface Bun {
    key: string;
    type: string;
    initialPrice: number;
    currentPrice: number;
    nextPrice: number;
    willBeThrownOut: boolean;
    timeUntilPriceChange: string;
}