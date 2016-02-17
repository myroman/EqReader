namespace f1.Business
open System

[<AllowNullLiteralAttribute>]
[<AbstractClass>]
type Trade(inst, symbol, cusip, descr, shares, price:decimal, commission:decimal) =
    member this.Inst = inst
    member this.Symbol = symbol
    member this.Cusip = cusip
    member this.Description = descr
    member this.Shares = shares
    member this.Price = price
    member this.Commission = commission
    member this.Gross = decimal(this.Shares) * this.Price
    
    abstract member Net: decimal
    
[<AllowNullLiteralAttribute>]    
type BuyTrade(inst, symbol, cusip, descr, shares, price:decimal, commission:decimal) =
    inherit Trade(inst, symbol, cusip, descr, shares, price, commission)
    
    override this.Net = this.Gross + this.Commission
    
[<AllowNullLiteralAttribute>]    
type SellTrade(inst, symbol, cusip, descr, shares, price:decimal, commission:decimal) =
    inherit Trade(inst, symbol, cusip, descr, shares, price, commission)
    
    override this.Net = this.Gross - this.Commission