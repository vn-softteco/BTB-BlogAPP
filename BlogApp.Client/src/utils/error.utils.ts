import { isAxiosError } from 'axios'
import { ApiError } from '@/types'

// TODO: Add other errors handling
export const getApiErrorMsg = (error: unknown): ApiError[] | undefined => {
  if (isAxiosError(error)) {
    if (error.response) {
      if (error.response.status === 400) {
        const errors = error.response.data.errors 
        let errorsList: ApiError[] = []

        if(!errors) return undefined

        for (const key of Object.keys(errors)) {
          const listOfName = errors[key] as string[]
          const detail: ApiError = { key, name: listOfName}

          errorsList.push(detail);
        }

        return errorsList
      }
    }
    return undefined
  }
}