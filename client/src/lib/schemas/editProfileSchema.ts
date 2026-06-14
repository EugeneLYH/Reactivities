import { z } from 'zod'
import { requiredString } from '../types/util/util'

export const editProfileSchema = z.object({
    displayName: requiredString("Display Name"),
    bio: z.string().optional(),
})

export type EditProfileSchema = z.input<typeof editProfileSchema>;