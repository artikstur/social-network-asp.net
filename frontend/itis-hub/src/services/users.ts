import axios from "axios";
import {API_BASE_URL} from "../constants.ts";
import {LoginUserRequest} from "../interfaces/requests/LoginUserRequest.ts";
import {RegisterUserRequest} from "../interfaces/requests/RegisterUserRequest.ts";
import { ApiResponse } from "../interfaces/responses/ApiResponse.ts";
import {GetUserByUsernameResponse} from "../interfaces/responses/GetUserByUsernameResponse.ts";
import {UserPublicData} from "../interfaces/models/UserPublicData.ts";

export const loginUser = async (data: LoginUserRequest): Promise<string> => {
    console.log(data)
    try {
        const response = await axios.post<ApiResponse<LoginResponse>>(`${API_BASE_URL}/api/users/login`, data);

        const {result, errors} = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        return result.token;
    } catch (error) {
        console.error('Error logging in:',);
        throw error;
    }
};

export const registerUser = async (data: RegisterUserRequest): Promise<string> => {
    console.log(data)
    try {
        const response = await axios.post<ApiResponse<string>>(`${API_BASE_URL}/api/users/register`, data);

        const {result, errors} = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        return result;
    } catch (error) {
        console.error('Error logging in:',);
        throw error;
    }
};

export const getUserDataByUserName = async (username: string): Promise<UserPublicData> => {
    console.log(username);
    try {
        const response = await axios.get<ApiResponse<GetUserByUsernameResponse>>(`${API_BASE_URL}/api/users/${username}`);

        const { result, errors } = response.data;

        if (errors.length > 0) {
            throw new Error(errors.join(", "));
        }

        const userData: UserPublicData = {
            id: result.id,
            userName: result.userName,
            email: result.email,
        };

        return userData;
    } catch (error) {
        console.error('Error fetching user data:', error);
        throw error;
    }
};