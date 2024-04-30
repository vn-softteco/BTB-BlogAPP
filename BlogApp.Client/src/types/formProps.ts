import { SubmitHandler, Control, UseFormHandleSubmit } from 'react-hook-form'
import { SxProps } from '@mui/system'
import { Theme } from '@mui/material'
import {
  SignInFormType,
  AddBlogPostFormType,
  AddCommentFormType
} from '.'

export type SignInFormProps = {
    control: Control<SignInFormType>
    handleSubmit: UseFormHandleSubmit<SignInFormType>
    onSubmit: SubmitHandler<SignInFormType>
    loading?: boolean
    sx?: SxProps<Theme>
}

export type AddBlogPostFormProps = {
  control: Control<AddBlogPostFormType>
  handleSubmit: UseFormHandleSubmit<AddBlogPostFormType>
  onSubmit: SubmitHandler<AddBlogPostFormType>
  loading?: boolean
  sx?: SxProps<Theme>
}

export type AddCommentFormProps = {
  control: Control<AddCommentFormType>
  handleSubmit: UseFormHandleSubmit<AddCommentFormType>
  onSubmit: SubmitHandler<AddCommentFormType>
  loading?: boolean
  sx?: SxProps<Theme>
}