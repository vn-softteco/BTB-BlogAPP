import { SubmitHandler, Control, UseFormHandleSubmit } from 'react-hook-form'
import { SxProps } from '@mui/system'
import { Theme } from '@mui/material'

export type FormProps<T extends object> = {
  control: Control<T>
  handleSubmit: UseFormHandleSubmit<T>
  onSubmit: SubmitHandler<T>
  loading?: boolean
  sx?: SxProps<Theme>
}