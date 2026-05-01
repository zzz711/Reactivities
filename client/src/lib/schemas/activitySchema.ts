import { z } from 'zod';

const requiredString = (field: string) =>  z.string({error: `${field} is required`}).min(1, {error: `${field} is required`});

export const activitySchema = z.object({
    title: requiredString('Title'),
    description: requiredString('Description'),
    category: requiredString('Category'),
    date: z.coerce.date({
        message: 'Date is required'
    }),
    location: z.object({
        venue: requiredString('Venue is required'),
        city: z.string().optional(),
        latitude: z.coerce.number(),
        longitude: z.coerce.number()
    })
});

export type ActivitySchema = z.input<typeof activitySchema>;