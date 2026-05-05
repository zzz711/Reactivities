import { format, type DateArg } from "date-fns";
import z from "zod";

export function formatDate(date: DateArg<Date>) {
    return format(date, 'MMM dd yyyy h:mm a')
}

export const requiredString = (field: string) =>  z.string({error: `${field} is required`}).min(1, {error: `${field} is required`});