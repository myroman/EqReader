namespace f1.Business
open System

[<AllowNullLiteralAttribute>]
type Trade(inst, symbol, cusip, descr, shares, price:decimal, commission:decimal, gross:decimal) =
    member this.Inst = inst
    member this.Symbol = symbol
    member this.Cusip = cusip
    member this.Description = descr
    member this.Shares = shares
    member this.Price = price
    member this.Commission = commission
    member this.Gross = gross
    member this.Net = gross + commission