import { EqualSquare, Merge, Scale, ShoppingBasket, SquarePercent } from "lucide-svelte";

export const EXPENSE_TYPES = [
    
    {
        key: "shared",
        name: "Shared",
        description: "Everyone pays an equal share.",
        usages: [
            "Shared-meal restaurant bills evenly.",
            "Household cleaning supplies.",
            "Household utilities (power, water, gas).",
            "Group subscriptions (Netflix, Spotify, family gym).",
            "Taxi/Uber rides."
        ],
        icon: EqualSquare
    },
    {
        key: "consolidated",
        name: "Consolidated",
        description: "Multiple people want to consolidate a payment so that all may spend less than buying separately. Payments differ based on what each person in the group wants and needs.",
        usages: [
            "Household internet where some people want fast internet and others want cheaper internet.",
            "Shared grocery delivery where some people wants to prioritise more premium items.",
            "Shared hotel accomodation where someone wants a more expensive room than the other.",
            "Buying appliances together where one person or some people prefer higher-quality versions.",
            "Carpool fuel costs where some insist that the more premium fuel is necessary."
        ],
        icon: Merge
    },
    {
        key: "weighted",
        name: "Weighted",
        description: "The expense is technically one payment, but some parties benefit more.",
        usages: [
            "Renting with housemates where some people have significantly larger bedrooms.",
            "Electricity bill split where one person hosts a full data center in their bedroom.",
            "Office expenses where some departments benefit significantly more, such as high end computers split between sales and engineering teams.",
            "Holiday accomodations where one person gets the master bedroom."
        ],
        icon: Scale
    },
    {
        key: "percentage",
        name: "Percentage",
        description: "The expense is split based on a known percentage amount or by an percentage calculated external to this app.",
        usages: [
            "Income-based expense splitting.",
            "Business partners splitting stock sales based on ownership",
            "Prior agreements for each party to contribute a set percentage to something."
        ],
        icon: SquarePercent
    },
    {
        key: "itemised",
        name: "Itemised (Australian spelling, Google it)",
        description: "Expenses are divvied out based on who is decidedly buying what item.",
        usages: [
            "Grocery shopping with separate personal items.",
            "Restaurant orders where each gets an individual set of dishes",
            "Group online shopping carts where each purchase an item for the total decrease in cost.",
            "Event tickets.",
            "Shared household shopping where ownership is explicit (own person wants their own toaster to avoid conflict after moving out)."
        ],
        icon: ShoppingBasket
    }

]