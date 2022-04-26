import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';

import { CheeseModel } from '../../models/cheese';
import paths from '../../routing/paths';
import cheesesApiService from '../../services/cheese-api.service';
import { formHelper } from '../../util/form-helper';

import styles from './styles.module.scss';

interface IProps {
  id: string | undefined;
}

const Edit: React.FC<IProps> = props => {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm();
  const navigate = useNavigate();
  const [cheese, setCheese] = useState<CheeseModel>();
  const [imgData, setImgData] = useState<any>(null);

  const onSubmit = (data: any) => {
    let fd = new FormData();
    if (!!data.image[0] && data.image[0] !== '/') {
      fd.append('image', data.image[0]);
    } else {
      if (!!cheese?.image) {
        const bytes = formHelper.b64toBlob(cheese?.image);
        fd.append('image', bytes);
      }
    }
    if (!!props.id) {
      fd.append('id', props.id);
    }
    fd.append('name', data.name);
    fd.append('color', data.color);
    fd.append('pricePerKg', data.pricePerKg);

    cheesesApiService.upsertCheese(fd).then((data: any) => {
      setCheese(data);
      navigate(`${paths.CHEESE_LIST}`);
    });

    return false;
  };

  const onChangePicture = (e: any) => {
    if (e.target.files[0]) {
      const reader: FileReader = new FileReader();
      reader.addEventListener('load', () => {
        setImgData(reader.result);
      });
      reader.readAsDataURL(e.target.files[0]);
    }
  };

  const deleteOnClick = (id: string) => () => {
    if (window.confirm(`Are you sure you want to delete?`)) {
      cheesesApiService.deleteCheese(id).then(() => {
        navigate(`${paths.CHEESE_LIST}`);
      });
    }
  };

  const backToList = () => {
    navigate(`${paths.CHEESE_LIST}`);
  };

  useEffect(() => {
    if (!!props.id && props.id !== '') {
      cheesesApiService.getCheese(props.id).then((data: any) => setCheese(data));
    }
  }, [props.id]);

  useEffect(() => {
    reset(cheese);
    if (!!cheese?.image) {
      setImgData(`data:image/jpeg;base64,${cheese?.image}`);
    } else {
      setImgData(null);
    }
  }, [cheese, reset]);

  return (
    <>
      Edit Cheese
      <div className={styles.editForm}>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className={styles.editField}>
            <label>
              Name:
              <input
                type="text"
                {...register('name', {
                  required: true,
                  pattern: /^[^\s]+[\sA-Za-z]+$/i,
                })}
                autoComplete="off"
                required
                maxLength={20}
              />
              {errors?.name?.type === 'pattern' && <div className={styles.fieldError}>Alphabetical characters only</div>}
            </label>
          </div>
          <div className={styles.editField}>
            <label>
              Color:
              <input
                type="text"
                {...register('color', { required: true, pattern: /^[^\s]+[\sA-Za-z]+$/i })}
                autoComplete="off"
                required
                maxLength={20}
              />
              {errors?.color?.type === 'pattern' && <div className={styles.fieldError}>Alphabetical characters only</div>}
            </label>
          </div>
          <div className={styles.editField}>
            <label>
              Price per Kg:
              <input
                type="number"
                step={0.01}
                {...register('pricePerKg', { pattern: /^[0-9]*(.[0-9]{0,2})?$/ })}
                autoComplete="off"
                required
                max={1000}
              />
            </label>
          </div>
          <div className={styles.editField}>
            <label>
              Image:
              <input {...register('image')} type="file" onChange={onChangePicture} />
              <div style={{ display: 'inline-block' }}>
                {!!imgData && <img alt="cheese pic" src={imgData} className={styles.cheeseImage} />}
              </div>
            </label>
          </div>
          <div className={styles.editField}>
            <div style={{ display: 'inline', paddingRight: 50 }}>
              <input type="button" value="Back" onClick={backToList} />
            </div>
            {!!props.id && (
              <div style={{ display: 'inline', paddingRight: 50 }}>
                <input type="button" value="Delete" onClick={deleteOnClick(props.id)} />
              </div>
            )}
            <div style={{ display: 'inline' }}>
              <input type="submit" value="Save" />
            </div>
          </div>
        </form>
      </div>
    </>
  );
};

export default Edit;
